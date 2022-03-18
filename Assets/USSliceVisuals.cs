using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USSliceVisuals : MonoBehaviour
{
    private Material material;
    private static readonly int ShowTexture = Shader.PropertyToID("_ShowTexture");

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void SetOutline(bool outline)
    {
        material.SetFloat(ShowTexture, outline ? 0 : 1);
    }
}
