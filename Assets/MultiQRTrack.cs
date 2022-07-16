using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiQRTrack : MonoBehaviour
{

    [Serializable]
    public struct QRCorrespondence
    {
        public GameObject targetQR;
        public GameObject sourceQR;
    }
    
    public List<QRCorrespondence> qrCorrespondences;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        List<Matrix4x4> desiredPoses = new List<Matrix4x4>();
        
        foreach (QRCorrespondence qrCorrespondence in qrCorrespondences)
        {
            Transform sourceQrTransform = qrCorrespondence.sourceQR.transform;
            Transform targetQrTransform = qrCorrespondence.targetQR.transform;
            Matrix4x4 sourceMatrix = sourceQrTransform.GetMatrix(Space.Self);
            Matrix4x4 targetMatrix = targetQrTransform.GetMatrix(Space.World);
            Matrix4x4 desiredPose = targetMatrix * sourceMatrix.inverse;
            desiredPoses.Add(desiredPose);
        }


        List<Quaternion> qList = new List<Quaternion>();
        List<Vector3> vList = new List<Vector3>();
        
        foreach (Matrix4x4 pose in desiredPoses)
        {
            qList.Add(pose.rotation);
            vList.Add(pose.GetPosition());
        }
        
        
        Quaternion averageQuat = Quaternion.identity ;
        
        float averageWeight = 1f / qList.Count ;
 
        for ( int i = 0; i < qList.Count; i ++ )
        {
            Quaternion q = qList [ i ] ;
 
            // based on [URL='https://forum.unity.com/members/lordofduct.66428/']lordofduct[/URL] response
            averageQuat *= Quaternion.Slerp ( Quaternion.identity, q, averageWeight ) ;
        }
        
        Vector3 averagePos = Vector3.zero ;
        
        foreach (Vector3 vector3 in vList)
        {
            averagePos += vector3;
        }
        averagePos /= vList.Count;
        
        
        transform.SetMatrix(Matrix4x4.TRS(averagePos, averageQuat, Vector3.one), Space.World);
    }
    

}

public static class LPPoseUtils {
    public static Pose Inverse(this Pose a)
    {
        Pose result = new Pose();
        result.rotation = Quaternion.Inverse(a.rotation);
        result.position = result.rotation * -a.position;
        return result;
    }
    
    /// <summary>
    /// Compose two poses, applying the provided one on top of the caller.
    /// </summary>
    /// <param name="a">Pose to compose upon.</param>
    /// <param name="b">Pose to compose over the first one.</param>
    public static Pose Multiply(this Pose a, in Pose b)
    {
        Pose result = new Pose();
        Multiply(a, b, ref result);
        return result;
    }

    /// <summary>
    /// Compose two poses, applying the caller on top of the provided pose.
    /// </summary>
    /// <param name="a">Pose to compose upon.</param>
    /// <param name="b">Pose to compose over the first one.</param>
    public static Pose Postmultiply(this Pose a, in Pose b)
    {
        Pose result = new Pose();
        Multiply(b, a, ref result);
        return result;
    }
    
    public static void Multiply(in Pose a, in Pose b, ref Pose result)
    {
        result.position = a.position + a.rotation * b.position;
        result.rotation = a.rotation * b.rotation;
    }
    
    
    public static Matrix4x4 GetMatrix(this Transform transform, Space space = Space.World)
    {
        if (space == Space.World)
        {
            return Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        }
        else
        {
            return Matrix4x4.TRS(transform.localPosition, transform.localRotation, Vector3.one);
        }
    }
    
        
    public static void SetMatrix(this Transform transform, Matrix4x4 matrix, Space space = Space.World)
    {
        if (space == Space.World)
        {
            transform.position = matrix.GetPosition();
            transform.rotation = matrix.rotation;
        }
        else
        {
            transform.localPosition = matrix.GetPosition();
            transform.localRotation = matrix.rotation;
        }
    }
}
