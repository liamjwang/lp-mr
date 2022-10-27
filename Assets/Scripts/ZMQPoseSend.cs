using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Capnp;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Proto.Services;
using UnityEngine;

public class ZMQPoseSend : MonoBehaviour
{

    private Proto.Messages.MeshStamped meshStamped;
    private ZMQConnection connect1;
    public string topic = "plan/set/";


    private void Start()
    {
        connect1 = ZMQConnection.GetOrCreateInstance();
    }


    public void SendMatrix()
    {
        
        Matrix4x4 matrix4X4 = transform.GetMatrix(Space.Self);
        // convert to list of floats
        float[] matrix = new[]
        {
            matrix4X4.m00, matrix4X4.m01, matrix4X4.m02, matrix4X4.m03,
            matrix4X4.m10, matrix4X4.m11, matrix4X4.m12, matrix4X4.m13,
            matrix4X4.m20, matrix4X4.m21, matrix4X4.m22, matrix4X4.m23,
            matrix4X4.m30, matrix4X4.m31, matrix4X4.m32, matrix4X4.m33
        };
        CapnpGen.Pose pose = new CapnpGen.Pose()
        {
            HomogeneousMatrix = matrix
        };
        
        // serialize to byte array
        var msg = MessageBuilder.Create();
        var root = msg.BuildRoot<CapnpGen.Pose.WRITER>();
        pose.serialize(root);
        var mems = new MemoryStream();
        var pump = new FramePump(mems);
        pump.Send(msg.Frame);
        byte[] bytes = mems.ToArray();

        connect1.Publish(topic,bytes);
    }
}
    
