using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// #if WINDOWS_UWP
// using Windows.Storage;
// #endif

public class ConnectIP : MonoBehaviour
{ 
    ZMQConnection ros;

    // ROS Connector
    // [SerializeField]
    // string rosIP;
    // public string ROSIP{ get => rosIP; set => rosIP = value; }
    public List<string> fallbackIPs = new List<string>();
    public List<int> fallbackPorts = new List<int>();
    public bool connectOnStart = true;
    private IEnumerator testFallbackIPsMultiTimeout;
    private IEnumerator fallbackIPsMultiTimeout;

    private void Awake()
    {
        ros = GetComponent<ZMQConnection>();
    }

    
    void Start()
    {
        var rosIP = ros.IPAddress;
// #if WINDOWS_UWP
//         // Get IP address from localSettings
//         var localSettings = ApplicationData.Current.LocalSettings;
//         if(localSettings.Values["IP"] != null){
//             rosIP = localSettings.Values["IP"].ToString();
//         }
// #endif
        rosIP = PlayerPrefs.GetString("IP", rosIP); 
        if (connectOnStart)
        {
            if (testFallbackIPsMultiTimeout != null)
            {
                StopCoroutine(testFallbackIPsMultiTimeout);
            }
            testFallbackIPsMultiTimeout = TestFallbackIPsMultiTimeout(rosIP);
            StartCoroutine(testFallbackIPsMultiTimeout);
        }
    }

    public void Connect(string inputIP)
    {
        var rosIP = inputIP; 
        PlayerPrefs.SetString("IP", rosIP);
        PlayerPrefs.Save();

        if (testFallbackIPsMultiTimeout != null)
        {
            StopCoroutine(testFallbackIPsMultiTimeout);
        }
        fallbackIPsMultiTimeout = TestFallbackIPsMultiTimeout(inputIP);
        StartCoroutine(fallbackIPsMultiTimeout);

// #if WINDOWS_UWP
//         // Save IP address to localSettings
//         var localSettings = ApplicationData.Current.LocalSettings;
//         localSettings.Values["IP"] = rosIP;   
// #endif
    }

    
    private IEnumerator TestFallbackIPsMultiTimeout(string inputIp)
    {
        if (ros.HasConnectionThread && !ros.HasConnectionError)
        {
            ros.Disconnect();
            ros.Connect(ros.IPAddress, ros.Port);
            yield return new WaitForSeconds(1f);
        }
        
        yield return TestFallbackIPs(inputIp, 0.7f);
        yield return TestFallbackIPs(inputIp, 2f);
    }
    
    private IEnumerator TestFallbackIPs(string inputIp, float timeout)
    {
        yield return TestAllPorts(inputIp, timeout);
        foreach (var fallbackIP in fallbackIPs)
        {
            if (ros.HasConnectionThread && !ros.HasConnectionError) yield break;
            yield return TestAllPorts(fallbackIP, timeout);
        }
    }

    private IEnumerator TestAllPorts(string ip, float timeout)
    {
        if (ip == "")
        {
            Debug.Log("IP is empty");
            yield break;
        }
        foreach (var fallbackPort in fallbackPorts)
        {
            if (ros.HasConnectionThread && !ros.HasConnectionError) yield break;
            ros.Disconnect();
            ros.Connect(ip, fallbackPort);
            yield return new WaitForSeconds(timeout);
        }
    }
}
