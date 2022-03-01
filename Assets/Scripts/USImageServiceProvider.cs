using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Proto.Services;
using UnityEngine;
using UnityEngine.Networking;

public class USImageServiceProvider : IServiceProviderBehavior
{
    
    public MeshRenderer targetMeshRenderer;
    private Proto.Messages.ImageStamped imageStamped;
    private bool flag;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private Texture2D texture;
    private string temporaryCachePath;

    private int lastUsed = 0;


    public override ServerServiceDefinition getServiceDefinition()
    {
        return (LiveUSImageService.BindService(new LiveUSImageServiceImpl(this)));
    }

    void Start()
    {
        temporaryCachePath = Application.temporaryCachePath;
    }

    IEnumerator GetText()
    {
        var path = "file://"+temporaryCachePath + "/image"+lastUsed+".png";

        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(uwr);
                targetMeshRenderer.material.SetTexture(MainTex, texture);
            }
        }
    }

    private void LateUpdate()
    {
        // if (imageStamped != null)
        // {
        //
        //     // targetMeshRenderer.material.SetTexture(MainTex, texture);
        //     imageStamped = null;
        // }
        if (flag)
        {
            StartCoroutine(GetText());
            flag = false;
        }
    }
    
    class LiveUSImageServiceImpl : LiveUSImageService.LiveUSImageServiceBase
    {
        private USImageServiceProvider _parent;
        public LiveUSImageServiceImpl(USImageServiceProvider parent)
        {
            _parent = parent;
        }
        public override Task<Empty> Update(Proto.Messages.ImageStamped request, ServerCallContext context)
        {
            try
            {
                int newWritten = (_parent.lastUsed + 1) % 4;
                
                ByteString byteString = request.Image.Data; // to texture
                // save to file in Application.temporaryCachePath/image.png
                var path = _parent.temporaryCachePath + "/image"+newWritten+".png";
                File.WriteAllBytes(path, byteString.ToByteArray());
                _parent.lastUsed = newWritten;
                _parent.flag = true;

                // Texture2D texture = new Texture2D(request.Image.Width, request.Image.Height);
                // PngBitmapDecoder 
                // texture.LoadImage(byteString.ToByteArray());
                // _parent.texture = texture;
                // FreeImage.FreeImage_Initialise();
            }  catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            return Task.FromResult(new Empty());
        }
    }
}
    
