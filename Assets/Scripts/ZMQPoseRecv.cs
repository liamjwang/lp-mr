using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Proto.Services;
using UnityEngine;

public class ZMQPoseRecv : MonoBehaviour
{

    private Proto.Messages.MeshStamped meshStamped;
    public string topic = "needle/pose/";


    private void Start()
    {
        ZMQConnection connect = ZMQConnection.GetOrCreateInstance();
        connect.Subscribe<CapnpGen.Pose>(topic, OnMeshRecv);
        
    }


    private void OnMeshRecv(CapnpGen.Pose pose)
    {
        var flat4x4FloatList = pose.HomogeneousMatrix;
        var flat4x4FloatArray = flat4x4FloatList.ToArray();
        var matrix4x4 = new Matrix4x4();
        matrix4x4[0, 0] = flat4x4FloatArray[0];
        matrix4x4[0, 1] = flat4x4FloatArray[1];
        matrix4x4[0, 2] = flat4x4FloatArray[2];
        matrix4x4[0, 3] = flat4x4FloatArray[3];
        matrix4x4[1, 0] = flat4x4FloatArray[4];
        matrix4x4[1, 1] = flat4x4FloatArray[5];
        matrix4x4[1, 2] = flat4x4FloatArray[6];
        matrix4x4[1, 3] = flat4x4FloatArray[7];
        matrix4x4[2, 0] = flat4x4FloatArray[8];
        matrix4x4[2, 1] = flat4x4FloatArray[9];
        matrix4x4[2, 2] = flat4x4FloatArray[10];
        matrix4x4[2, 3] = flat4x4FloatArray[11];
        matrix4x4[3, 0] = flat4x4FloatArray[12];
        matrix4x4[3, 1] = flat4x4FloatArray[13];
        matrix4x4[3, 2] = flat4x4FloatArray[14];
        matrix4x4[3, 3] = flat4x4FloatArray[15];
        
        transform.SetMatrix(matrix4x4, Space.Self);
        // transform.localPosition = new Vector3((float)pose.Position.X, (float)pose.Position.Y, (float)pose.Position.Z);
        // transform.localRotation = new Quaternion((float)pose.Orientation.X, (float)pose.Orientation.Y, (float)pose.Orientation.Z, (float)pose.Orientation.W);
    }
}
    
