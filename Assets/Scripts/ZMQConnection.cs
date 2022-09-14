using System;
using System.Collections.Generic;
using System.IO;
using AsyncIO;
using Capnp;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using UnityEngine.Serialization;

public class ZMQConnection : MonoBehaviour
{
    
    private static ZMQConnection _instance;
    public static ZMQConnection GetOrCreateInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<ZMQConnection>();
            if (_instance != null)
                return _instance;

            _instance = new GameObject("ZMQConnection").AddComponent<ZMQConnection>();
        }
        return _instance;
    }
    
    public string IPAddress = "localhost";
    public int Port = 9090;
    public bool HasConnectionError;
    public bool HasConnectionThread;
    
    private SubscriberSocket subscriber;
    private readonly Dictionary<string, List<Action<ICapnpSerializable>>> callbacks = new();
    private readonly Dictionary<string, Type> types = new();
    private readonly HashSet<string> subscribedTopics = new();

    public void Connect(string ip, int port)
    {
        Disconnect();
        subscriber = new SubscriberSocket($"tcp://{ip}:{port}");
        foreach (var topic in subscribedTopics)
        {
            subscriber.Subscribe(topic);
            Debug.Log($"Subscribed to {topic}");
        }
        Debug.Log($"Connecting to {ip}:{port}");
    }

    public void Disconnect()
    {
        subscriber?.Dispose();
    }
    

    private void Start()
    {
        ForceDotNet.Force();
    }

    private void LateUpdate() // could also be in update
    {
        if (subscriber != null)
        {
            
            var receivedMessages = new Dictionary<string, byte[]>();
        
            List<byte[]> msg = null;
            for (var count = 0; count < 100; count++)
            {
                if (!subscriber.TryReceiveMultipartBytes(ref msg)) break;
                if (msg.Count != 2) continue;
                receivedMessages[System.Text.Encoding.ASCII.GetString(msg[0])] = msg[1];
            }

            foreach (var pair in receivedMessages)
            {
                var callback = callbacks.GetValueOrDefault(pair.Key, null);
                if (callback == null) continue;
                Type type = types.GetValueOrDefault(pair.Key, null);
                if (type == null) continue;
            
                MemoryStream memoryStream = new(pair.Value);
                WireFrame readSegments = Framing.ReadSegments(memoryStream);
                var deserializer = DeserializerState.CreateRoot(readSegments);
                if (Activator.CreateInstance(type) is not ICapnpSerializable message) continue;
                message.Deserialize(deserializer);
                foreach (var c in callback)
                {
                    c(message);
                }
            }
        }

    }
    
    public void Subscribe<T>(string topic, Action<T> callback) where T : ICapnpSerializable
    {
        subscribedTopics.Add(topic);
        subscriber?.Subscribe(topic); // TODO: okay to call multiple times?
        Debug.Log($"Subscribed to {topic}");
        
        if (types.ContainsKey(topic))
        {
            if (types[topic] != typeof(T))
            {
                Debug.LogError($"Trying to subscribe to topic {topic} with a different type than before");
                return;
            }
        }
        else
        {
            types[topic] = typeof(T);
        }
        
        var callbackList = callbacks.GetValueOrDefault(topic, null);
        if (callbackList == null)
        {
            callbackList = new List<Action<ICapnpSerializable>>();
            callbacks[topic] = callbackList;
        }
        callbackList.Add(msg =>
        {
            callback((T) msg);
        });
    }

    private void OnDestroy()
    {
        Disconnect();
        NetMQConfig.Cleanup(false);
    }
}
