using System.Collections;
using System.Collections.Generic;
using QRTracking;
using UnityEngine;

public class QRDataFollower : MonoBehaviour
{
    public string data;
    private QRCodeDisplay codeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        codeDisplay = QRCodesVisualizer.Instance.GetQRCode(data);
    }

    // Update is called once per frame
    void Update()
    {
        if (codeDisplay.qrCode != null)
        {
            Transform myTransform = transform;
            Transform qrTransform = codeDisplay.transform;
            myTransform.position = qrTransform.position;
            myTransform.rotation = qrTransform.rotation;
            myTransform.localScale = Vector3.one;
        }
        
    }
}
