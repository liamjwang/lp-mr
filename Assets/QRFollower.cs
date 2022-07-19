using System;
using System.Collections;
using System.Collections.Generic;
using QRTracking;
using UnityEngine;
using Util;

public class QRFollower : MonoBehaviour
{
    public string data;
    [HideInInspector]
    public long lastUpdated;

    private Matrix4x4 lastPose;
    
    // Start is called before the first frame update
    void Start()
    {
        QRCodesDataEvents.Instance.Subscribe(data, OnDataAction);
    }

    private void OnDataAction(QRCodesDataEvents.QRActionData obj)
    {
        Microsoft.MixedReality.QR.QRCode qrCode = obj.qrCode;
        SpatialGraphCoordinateSystem spatialGraphCoordinateSystem = gameObject.GetOrAddComponent<QRTracking.SpatialGraphCoordinateSystem>();
        spatialGraphCoordinateSystem.Id = qrCode.Id;
        lastUpdated = qrCode.SystemRelativeLastDetectedTime.Ticks;
        Debug.Log("QRFollower Updated: " + qrCode.Id + " " + qrCode.Data);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            Matrix4x4 pose = transform.GetMatrix();
            if (lastPose != pose) // exact
            {
                lastPose = pose;
                lastUpdated = Time.frameCount;
            }
        }
    }
}
