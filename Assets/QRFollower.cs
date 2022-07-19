using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRFollower : MonoBehaviour
{
    public string data;
    public DateTimeOffset lastUpdated;

    private Matrix4x4 lastPose;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 pose = transform.GetMatrix();
        if (lastPose != pose) // exact
        {
            lastPose = pose;
            lastUpdated = DateTimeOffset.Now;
        }
    }
}
