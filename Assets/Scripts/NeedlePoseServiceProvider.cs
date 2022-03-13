using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Proto.Services;
using UnityEngine;
using UnityEngine.Networking;
using Pose = Proto.Messages.Pose;

public class NeedlePoseServiceProvider : IServiceProviderBehavior
{
    
    public Transform targetTransform;

    private Pose lastPose;
    private bool flag;

    private int lastUsed = 0;


    public override ServerServiceDefinition getServiceDefinition()
    {
        return (NeedlePoseService.BindService(new NeedlePoseServiceImpl(this)));
    }

    void Start()
    {

    }

    private void LateUpdate()
    {

        if (flag)
        {
            if (lastPose != null)
            {
                targetTransform.localPosition = new Vector3(lastPose.Position.X, lastPose.Position.Y, lastPose.Position.Z);
                targetTransform.localRotation = new Quaternion(lastPose.Orientation.X, lastPose.Orientation.Y, lastPose.Orientation.Z, lastPose.Orientation.W);
            }
            flag = false;
        }
    }
    
    class NeedlePoseServiceImpl : NeedlePoseService.NeedlePoseServiceBase
    {
        private NeedlePoseServiceProvider _parent;
        public NeedlePoseServiceImpl(NeedlePoseServiceProvider parent)
        {
            _parent = parent;
        }
        public override Task<Empty> Update(Proto.Messages.PoseStamped request, ServerCallContext context)
        {
            try
            {
                int newWritten = (_parent.lastUsed + 1) % 4;
                
                _parent.lastPose = request.Pose;
                _parent.flag = true;
            }  catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            return Task.FromResult(new Empty());
        }
    }
}
    
