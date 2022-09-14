using System.Linq;
using UnityEngine;

public class ZMQImgRecv : MonoBehaviour
{
    public MeshFilter targetMesh;

    private Proto.Messages.MeshStamped meshStamped;
    

    private void Start()
    {
        ZMQConnection connect = ZMQConnection.GetOrCreateInstance();
        connect.Subscribe<CapnpGen.ImageStamped>("live_b_jpeg/", OnImgRecv);
    }

    private void OnImgRecv(CapnpGen.ImageStamped imgStamped)
    {
        byte[] bytes = imgStamped.Image.Data.ToArray();
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);
        targetMesh.GetComponent<Renderer>().material.mainTexture = tex;
    }
}