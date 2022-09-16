using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Proto.Services;
using UnityEngine;

public class ZMQMeshRecv : MonoBehaviour
{
    public MeshFilter targetMesh;

    
    private Proto.Messages.MeshStamped meshStamped;
    private Mesh myMesh = new Mesh();
    

    private void Start()
    {
        myMesh = new Mesh();
        targetMesh.mesh = myMesh;
        
        ZMQConnection connect = ZMQConnection.GetOrCreateInstance();
        connect.Subscribe<CapnpGen.Mesh>("volume/mesh/", OnMeshRecv);
        
    }

    private void OnMeshRecv(CapnpGen.Mesh mesh)
    {
        myMesh.Clear();

        Vector3[] vertices = new Vector3[mesh.Vertices.Count / 3];
        for (int i = 0; i < mesh.Vertices.Count; i += 3)
        {
            vertices[i / 3] = new Vector3(mesh.Vertices[i], mesh.Vertices[i + 1], mesh.Vertices[i + 2]);
        }

        myMesh.vertices = vertices;
        myMesh.triangles = mesh.Faces.ToArray();
        myMesh.RecalculateNormals();
    }
    //
    // class USMeshServiceImpl : USMeshService.USMeshServiceBase
    // {
    //     private MeshServiceProvider _parent;
    //     public USMeshServiceImpl(MeshServiceProvider parent)
    //     {
    //         _parent = parent;
    //     }
    //     public override Task<Empty> Update(Proto.Messages.MeshStamped request, ServerCallContext context)
    //     {
    //         try
    //         {
    //             _parent.meshStamped = request;
    //     
    //             Debug.Log("Mesh received");
    //         }  catch (Exception e)
    //         {
    //             Debug.LogError(e);
    //         }
    //
    //         return Task.FromResult(new Empty());
    //     }
    // }
}
    
