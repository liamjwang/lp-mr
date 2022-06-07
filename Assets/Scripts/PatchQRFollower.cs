using System.Collections;
using System.Collections.Generic;
using QRTracking;
using UnityEngine;

public class PatchQRFollower : SingleQRFollower
{
    private QRCode code;
    public GameObject quad;
    public GameObject center;
    private Vector3 cameraTransform;
    public float size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (code != null)
        {
            Transform myTransform = transform;
            Transform qrTransform = code.transform;
            if (QrSizeCompensation)
            {
                myTransform.position = (qrTransform.position - cameraTransform)* (size/code.PhysicalSize) + cameraTransform;
            }
            else
            {
                myTransform.position = qrTransform.position;
            }
            myTransform.rotation = qrTransform.rotation;
            
            quad.transform.localPosition = new Vector3(code.PhysicalSize / 2.0f, code.PhysicalSize / 2.0f, 0.0f);
            quad.transform.localScale = new Vector3(code.PhysicalSize, code.PhysicalSize, 1.0f);
            center.transform.localPosition = new Vector3(code.PhysicalSize / 2.0f, code.PhysicalSize / 2.0f, 0.0f);
            transform.localScale = Vector3.one;
        }
        
    }

    public bool QrSizeCompensation { get; set; } = false;

    public override void Follow(QRCode qrCode)
    {
        code = qrCode;
    }

    public override void UpdateLastCameraPose(Vector3 cameraTransform)
    {
        this.cameraTransform = cameraTransform;
    }
}
