using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Collections.Generic;
using System.IO;
using AsyncIO;
using Capnp;
using Vector3 = CapnpGen.Vector3;

public class ConnectionClient : MonoBehaviour
{
    public string topic = "text";
    
    private SubscriberSocket subscriber;
    private ZMQConnection connection;

    private void Start()
    {
        connection = ZMQConnection.GetOrCreateInstance();

        connection.Subscribe(topic, (Vector3 vector) =>
        {
            Debug.Log($"Connection client recv: {vector}");
            Debug.Log($"Decoded: {vector.X} {vector.Y} {vector.Z}");
        });
    }

}