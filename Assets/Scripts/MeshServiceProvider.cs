using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Proto.Services;
using UnityEngine;

public class MeshServiceProvider : IServiceProviderBehavior
{
    public MeshFilter targetMesh;

    
    private Proto.Messages.MeshStamped meshStamped;
    private Mesh myMesh;

    public override ServerServiceDefinition getServiceDefinition()
    {
        return (USMeshService.BindService(new USMeshServiceImpl(this)));
    }

    private void Start()
    {
        myMesh = new UnityEngine.Mesh();
        targetMesh.mesh = myMesh;
    }

    private void LateUpdate()
    {
        if (meshStamped != null)
        {
            myMesh.Clear();
            // log request.Faces
            Proto.Messages.Mesh mesh = meshStamped.Mesh;
            Debug.Log(mesh.Faces);
            Debug.Log(mesh.Vertices);

            UnityEngine.Vector3[] vertices = new UnityEngine.Vector3[mesh.Vertices.Count / 3];
            for (int i = 0; i < mesh.Vertices.Count; i += 3)
            {
                vertices[i / 3] = new UnityEngine.Vector3(mesh.Vertices[i], mesh.Vertices[i + 1], mesh.Vertices[i + 2]);
            }

            myMesh.vertices = vertices;
            myMesh.triangles = mesh.Faces.ToArray();
            myMesh.RecalculateNormals();
            mesh = null;
        }
    }
    
    class USMeshServiceImpl : USMeshService.USMeshServiceBase
    {
        private MeshServiceProvider _parent;
        public USMeshServiceImpl(MeshServiceProvider parent)
        {
            _parent = parent;
        }
        public override Task<Empty> Update(Proto.Messages.MeshStamped request, ServerCallContext context)
        {
            try
            {
                _parent.meshStamped = request;
        
                Debug.Log("Mesh received");
            }  catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            return Task.FromResult(new Empty());
        }
    }
}
    
