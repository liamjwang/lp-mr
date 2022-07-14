using System;
using System.Collections;
using System.Collections.Generic;
using QRTracking;
using UnityEngine;

public class QRDataFollower : MonoBehaviour
{
    public string data;
    private QRCodeDisplay codeDisplay;

    private void Awake()
    {
        codeDisplay = QRCodesVisManager.Instance.GetQRCode(data);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (codeDisplay == null)
        {
            Debug.Log("codeDisplay is null");
        }

        if (codeDisplay.qrCode == null)
        {
            Debug.Log("codeDisplay.qrCode is null");
        }

        if (codeDisplay.transform == null)
        {
            Debug.Log("codeDisplay.transform is null");
        }
        
        if (codeDisplay != null)
        {
            Transform myTransform = transform;
            Transform qrTransform = codeDisplay.transform;
            myTransform.position = qrTransform.position;
            myTransform.rotation = qrTransform.rotation;
            myTransform.localScale = Vector3.one;
        }
        
    }
}
