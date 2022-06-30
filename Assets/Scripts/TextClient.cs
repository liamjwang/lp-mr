using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Collections.Generic;
using System.IO;
using AsyncIO;
using Capnp;
using Vector3 = CapnpGen.Vector3;

public class TextClient : MonoBehaviour
{
    public string connectionString = "tcp://localhost:5555";
    public string socket = "text";
    
    private SubscriberSocket subscriber;

    private void Start()
    {
        ForceDotNet.Force();

        subscriber = new SubscriberSocket(connectionString);
        subscriber.Subscribe(socket);
    }

    private void LateUpdate() // could also be in update
    {
        List<byte[]> msg = null;
        // batch receive up to 100 messages in case publisher is faster than this client's rate
        // this empties the zmq incoming message queue quickly so that we always have the latest message
        for (var count = 0; count < 100; count++)
        {
            if (!subscriber.TryReceiveMultipartBytes(ref msg)) break;
        }

        if (msg != null)
        {
            byte[] topic = msg[0]; // first frame is the topic
            byte[] contents = msg[1]; // second frame is the contents

            // TODO: Use this: https://capnproto.github.io/pycapnp/quickstart.html#byte-segments

            MemoryStream memoryStream = new MemoryStream(contents);
            WireFrame readSegments = Framing.ReadSegments(memoryStream);
            var deserializer = DeserializerState.CreateRoot(readSegments);
            var reader = new CapnpGen.Vector3.READER(deserializer);
            Vector3 asdf = new CapnpGen.Vector3();


            Debug.Log($"Decoded: {reader.X} {reader.Y} {reader.Z}");
        }
    }

    private void OnDestroy()
    {
        subscriber?.Dispose();
        NetMQConfig.Cleanup(false);
    }
}