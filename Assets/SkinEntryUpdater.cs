using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SkinEntryUpdater : MonoBehaviour
{
    public GameObject needleTip;
    public GameObject planEntry;
    public Material skinMaterial;

    private static readonly int NeedleTipPosition = Shader.PropertyToID("_NeedleTipPosition");
    private static readonly int NeedleTipDirection = Shader.PropertyToID("_NeedleTipDirection");
    private static readonly int TargetPosition = Shader.PropertyToID("_TargetPosition");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        skinMaterial.SetVector(NeedleTipPosition, needleTip.transform.position);
        skinMaterial.SetVector(NeedleTipDirection, -needleTip.transform.up);
        skinMaterial.SetVector(TargetPosition, planEntry.transform.position);
    }
    
}
