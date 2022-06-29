using UnityEngine;

public class ZMQConnection : MonoBehaviour
{
    public string RosIPAddress = "localhost";
    public int RosPort = 9090;
    public bool HasConnectionError;
    public bool HasConnectionThread;

    public void Connect(string rosIP, int rosPort)
    {
        Debug.Log($"Connecting to {rosIP}:{rosPort}");
    }

    public void Disconnect()
    {
        Debug.Log("Disconnecting");
    }
}
