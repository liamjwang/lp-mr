using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class SliderSnapper : MonoBehaviour
{
    
    public PinchSlider slider;
    public SliderSounds sliderSounds;
    
    public SliderSounds SliderSounds
    {
        get
        {
          if (sliderSounds == null)
          {
            sliderSounds = GetComponent<SliderSounds>();
          }
          return sliderSounds;
        }
        set => sliderSounds = value;
    }
    
    public int numSnapPoints = 2;
    
    public int NumSnapPoints
    {
        get => numSnapPoints;
        set
        {
            numSnapPoints = value;
            SliderSounds.SetFieldValue("tickEvery", 1f / (numSnapPoints-1));
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<PinchSlider>();
        }
        
        slider.OnInteractionEnded.AddListener(SnapSlider);
        
        SliderSounds.SetFieldValue("tickEvery", 1f / (numSnapPoints-1));
    }

    private void SnapSlider(SliderEventData arg0)
    {
        slider.SliderValue = Mathf.Round(slider.SliderValue * (numSnapPoints-1)) / (numSnapPoints-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
