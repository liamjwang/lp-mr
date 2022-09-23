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
    

    private void Start()
    {
        ZMQConnection connect = ZMQConnection.GetOrCreateInstance();
        connect.Subscribe<CapnpGen.Pose>("needle/pose/", OnMeshRecv);
        
    }


    private void OnMeshRecv(CapnpGen.Pose pose)
    {
        transform.localPosition = new Vector3((float)pose.Position.X, (float)pose.Position.Y, (float)pose.Position.Z);
        transform.localRotation = new Quaternion((float)pose.Orientation.X, (float)pose.Orientation.Y, (float)pose.Orientation.Z, (float)pose.Orientation.W);
    }
}
    
