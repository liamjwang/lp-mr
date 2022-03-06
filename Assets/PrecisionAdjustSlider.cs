using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

public class PrecisionAdjustSlider : MonoBehaviour
{
    public TextMeshPro text;
    public SliderSnapper slider;
    public PinchSlider pinchSlider;
    public SnapAdjustController snapAdjustController;
    
    public int maxPrecision = -2;
    public int minPrecision = -5;
    
    public int displayPrecisionRelativeTo = -3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pinchSlider.OnValueUpdated.AddListener(UpdateText);
        slider.NumSnapPoints = maxPrecision - minPrecision + 1;
    }

    private void UpdateText(SliderEventData arg0)
    {
        float floatZeroToOne = arg0.NewValue;
        int precision = (int) (Mathf.Lerp(minPrecision, maxPrecision, floatZeroToOne)-0.5f);
        text.text = $"{Mathf.Pow(10, precision - displayPrecisionRelativeTo)} mm";
        
        snapAdjustController.snapSigFigs = precision;
    }
}
