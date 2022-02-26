// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using Grpc.Core;
// using Proto.Services;
// using Proto.Messages;
// using System.Threading.Tasks;
// using Google.Protobuf.WellKnownTypes;
// using Pose = Proto.Messages.Pose;
//
// public class HelloMessageHandler : MonoBehaviour
// {
//     public int ServerPort;
//     // public TMPro.TMP_Text TargetText;
//     private string _receivedMessage = null;
//
//     private Server _grpcServer;
//     private UnityEngine.Mesh myMesh;
//     private Proto.Messages.Mesh _protoMesh;
//     private Pose _receivedPose;
//     public MeshFilter targetMesh;
//
//     void OnEnable()
//     {
//         _grpcServer = new Server
//         {
//             Ports = { new ServerPort("0.0.0.0", ServerPort, ServerCredentials.Insecure) }
//         };
//         _grpcServer.Services.Add(MeshService.BindService(new MeshServiceImpl(this)));
//         _grpcServer.Services.Add(PoseService.BindService(new PoseServiceImpl(this)));
//         _grpcServer.Start();
//         Debug.Log($"GRPC Server running on port {ServerPort}");
//
//         myMesh = new UnityEngine.Mesh();
//         targetMesh.mesh = myMesh;
//         // gameObject.GetComponent<MeshFilter>().mesh = myMesh;
//     }
//
//     void OnDisable()
//     {
//         _grpcServer.ShutdownAsync().Wait();
//     }
//
//     void Update()
//     {
//         // if (_receivedMessage != null)
//         // {
//         //     TargetText.text = _receivedMessage;
//         //     _receivedMessage = null;
//         // }
//         
//         if (_protoMesh != null)
//         {
//             myMesh.Clear();
//             // log request.Faces
//             Debug.Log(_protoMesh.Faces);
//             Debug.Log(_protoMesh.Vertices);
//
//             UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[_protoMesh.Vertices.Count / 3];
//             for (int i = 0; i < _protoMesh.Vertices.Count; i += 3)
//             {
//                 vertices[i / 3] = new UnityEngine.Vector3(_protoMesh.Vertices[i], _protoMesh.Vertices[i + 1], _protoMesh.Vertices[i + 2]);
//             }
//
//             myMesh.vertices = vertices;
//             myMesh.triangles = _protoMesh.Faces.ToArray();
//             myMesh.RecalculateNormals();
//             _protoMesh = null;
//         }
//         
//         if (_receivedPose != null)
//         {
//             gameObject.transform.position = new UnityEngine.Vector3(_receivedPose.Position.X, _receivedPose.Position.Y, _receivedPose.Position.Z);
//             gameObject.transform.rotation = new UnityEngine.Quaternion(_receivedPose.Orientation.X, _receivedPose.Orientation.Y, _receivedPose.Orientation.Z, _receivedPose.Orientation.W);
//             _receivedPose = null;
//         }
//     }
//
//     class MeshServiceImpl : MeshService.MeshServiceBase
//     {
//         private HelloMessageHandler _parent;
//         public MeshServiceImpl(HelloMessageHandler parent)
//         {
//             _parent = parent;
//         }
//         public override Task<Empty> SendMesh(Proto.Messages.Mesh request, ServerCallContext context)
//         {
//             try
//             {
//                 _parent._receivedMessage = "cool";
//                 _parent._protoMesh = request;
//
//                 Debug.Log("Mesh received");
//             }  catch (System.Exception e)
//             {
//                 Debug.LogError(e);
//             }
//
//             return Task.FromResult(new Empty());
//         }
//     }
//     
//     class PoseServiceImpl : PoseService.PoseServiceBase
//     {
//         private HelloMessageHandler _parent;
//         public PoseServiceImpl(HelloMessageHandler parent)
//         {
//             _parent = parent;
//         }
//         public override Task<Empty> UpdatePose(Proto.Messages.Pose request, ServerCallContext context)
//         {
//             try
//             {
//                 _parent._receivedPose = request;
//             }
//             catch (System.Exception e)
//             {
//                 Debug.LogError(e);
//             }
//
//             return Task.FromResult(new Empty());
//         }
//     }
// }
