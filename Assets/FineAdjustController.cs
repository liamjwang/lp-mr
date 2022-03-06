using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEditor;
using UnityEngine;
using ManipulationEventData = Microsoft.MixedReality.OpenXR.ManipulationEventData;

public static class Hmmm
{
    public static T GetFieldValue<T>(this object obj, string name) {
        var field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        return (T)field?.GetValue(obj);
    }
    public static void SetFieldValue<T>(this object obj, string name, T value) {
        var field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        field?.SetValue(obj, value);
    }
    public static T GetPropertyValue<T>(this object obj, string name) {
        var field = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        return (T)field?.GetValue(obj);
    }
    
}
public class FineAdjustController : MonoBehaviour
{

    public ObjectManipulator manipulator;
    
    public MeshRenderer meshRenderer;
    private IMixedRealityPointer mixedRealityPointer;

    private float InitialMoveLerpTime;
    private float InitialScaleLerpTime;
    private float InitialRotateLerpTime;
    public float minLerpK = 0.5f;
    public float maxLerpK = 0f;


    // Start is called before the first frame update
    void Start()
    {
        manipulator.OnManipulationStarted.AddListener(ManipulationStart);
        
        InitialMoveLerpTime = manipulator.MoveLerpTime;
        InitialScaleLerpTime = manipulator.ScaleLerpTime;
        InitialRotateLerpTime = manipulator.RotateLerpTime;
    }

    // Update is called once per frame
    void Update()
    {
        var twoHandManip = manipulator.GetPropertyValue<bool>("IsTwoHandedManipulationEnabled");
        var oneHandManip = manipulator.GetPropertyValue<bool>("IsOneHandedManipulationEnabled");
        
        // meshRenderer.material.color = Color.white;
        if (twoHandManip)
        {
            // meshRenderer.material.color = Color.yellow;
            manipulator.MoveLerpTime = 0;
            // manipulator.ScaleLerpTime = 0;
            // manipulator.RotateLerpTime = 0;

        }
        
        if (oneHandManip)
        {
            float distance = (mixedRealityPointer.Position-transform.position).magnitude;
            // meshRenderer.material.color = new Color(distance, 0, 0);
            float coef = Mathf.Lerp(minLerpK, maxLerpK, Mathf.InverseLerp(0f, 1f, distance));
            manipulator.MoveLerpTime = InitialMoveLerpTime * coef;
            // manipulator.ScaleLerpTime = InitialScaleLerpTime * coef;
            // manipulator.RotateLerpTime = InitialRotateLerpTime * coef;
        }


    }
    
    public void ManipulationStart(Microsoft.MixedReality.Toolkit.UI.ManipulationEventData arg0)
    {
        mixedRealityPointer = arg0.Pointer;
    }
}
