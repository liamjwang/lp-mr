using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.QR;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

public class QRSizeAdjuster : MonoBehaviour
{
    
    public PinchSlider slider;
    public PatchQRFollower qrCode;
    public TextMeshPro label;
    
    public float minSize = 0.01f;
    public float maxSize = 0.05f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<PinchSlider>();
        }
        
    }

    public void SliderUpdated(SliderEventData eventData)
    {
        float qrCodeSize = Mathf.Lerp(minSize, maxSize, slider.SliderValue);
        qrCode.size = qrCodeSize;
        label.text = qrCodeSize.ToString("0.000");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
