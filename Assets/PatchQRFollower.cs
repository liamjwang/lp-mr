using System.Collections;
using System.Collections.Generic;
using QRTracking;
using UnityEngine;

public class PatchQRFollower : SingleQRFollower
{
    private QRCode code;
    public GameObject quad;
    public GameObject center;

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
            myTransform.position = qrTransform.position;
            myTransform.rotation = qrTransform.rotation;
            
            quad.transform.localPosition = new Vector3(code.PhysicalSize / 2.0f, code.PhysicalSize / 2.0f, 0.0f);
            quad.transform.localScale = new Vector3(code.PhysicalSize, code.PhysicalSize, 1.0f);
            center.transform.localPosition = new Vector3(code.PhysicalSize / 2.0f, code.PhysicalSize / 2.0f, 0.0f);
        }
        
    }

    public override void Follow(QRCode qrCode)
    {
        code = qrCode;
    }
}
