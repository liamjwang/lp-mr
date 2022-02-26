using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Grpc.Core;
using Proto.Services;
using Proto.Messages;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Pose = Proto.Messages.Pose;

public class GrpcManager : MonoBehaviour
{
    public int ServerPort = 9090;
    public List<IServiceProviderBehavior> ServiceProviders;

    private Server _grpcServer;
    
    void OnEnable()
    {
        _grpcServer = new Server
        {
            Ports = { new ServerPort("0.0.0.0", ServerPort, ServerCredentials.Insecure) }
        };
        foreach (var serviceProvider in ServiceProviders)
        {
            _grpcServer.Services.Add(serviceProvider.getServiceDefinition());
        }
        _grpcServer.Start();
        Debug.Log($"GRPC Server running on port {ServerPort}");
    }

    void OnDisable()
    {
        _grpcServer.ShutdownAsync().Wait();
    }
}

public abstract class IServiceProviderBehavior : MonoBehaviour
{
    public abstract ServerServiceDefinition getServiceDefinition();
}