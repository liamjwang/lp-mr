using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#if WINDOWS_UWP
using Windows.Storage;
#endif

public class ConnectIP : MonoBehaviour
{ 
    ZMQConnection ros;

    // ROS Connector
    [SerializeField]
    string rosIP;
    public string ROSIP{ get => rosIP; set => rosIP = value; }
    public bool connectOnStart = true;

    private void Awake()
    {
        ros = GetComponent<ZMQConnection>();
    }

    
    void Start()
    {
        rosIP = ros.RosIPAddress;
#if WINDOWS_UWP
        // Get IP address from localSettings
        var localSettings = ApplicationData.Current.LocalSettings;
        if(localSettings.Values["IP"] != null){
            rosIP = localSettings.Values["IP"].ToString();
        }
#endif
        if (connectOnStart)
        {
            ros.Connect(rosIP, ros.RosPort);
        }
    }

    public void Connect(string inputIP)
    {
        rosIP = inputIP; 
        ros.Disconnect();
        ros.Connect(rosIP, ros.RosPort);

#if WINDOWS_UWP
        // Save IP address to localSettings
        var localSettings = ApplicationData.Current.LocalSettings;
        localSettings.Values["IP"] = rosIP;   
#endif
    }
}
