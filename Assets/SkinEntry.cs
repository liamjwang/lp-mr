using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SkinEntry : MonoBehaviour
{

    public Transform skin;

    public Transform needle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        new Plane(skin.up, skin.position).Raycast(new Ray(needle.position, needle.forward), out float distance);
        Vector3 point = needle.position + needle.forward * distance;
        transform.position = point;
    }
}
