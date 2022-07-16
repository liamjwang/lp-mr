using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiQRTrack : MonoBehaviour
{

    [Serializable]
    public struct QRCorrespondence
    {
        public GameObject targetQR;
        public GameObject sourceQR;
    }
    
    public List<QRCorrespondence> qrCorrespondences;
    public float kPosWeight = 1;
    public float kRotWeight = 1;
    public float lr = 0.02f;
    public float lrDelta = 0.01f;
    public float lrMin = 0.0001f;
    public float errorDisplay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < 1000; i++)
        {
            StepOptimizer();
        }
    }

    private void StepOptimizer()
    {
        // Dumb optimizer that walks randomly instead of using gradient descent
        Quaternion rotationUniform = Random.rotationUniform;

        float ogError = Loss();

        Quaternion quaternion = Quaternion.Slerp(Quaternion.identity, rotationUniform, lr * 10);
        Matrix4x4 randomPose = Matrix4x4.TRS(Random.insideUnitSphere * lr, quaternion, new Vector3(1, 1, 1));
        Matrix4x4 ogPose = transform.GetMatrix();
        transform.SetMatrix(randomPose * ogPose);

        float newError = Loss();

        errorDisplay = ogError;

        if (newError > ogError)
        {
            transform.SetMatrix(ogPose);
            lr *= 1 - lrDelta;
        }
        else
        {
            lr *= 1 + lrDelta;
        }

        if (lr < lrMin)
        {
            lr = lrMin;
        }
    }

    private float Loss()
    {
        float totalError = 0;
        foreach (QRCorrespondence qrCorrespondence in qrCorrespondences)
        {
            Transform sourceQrTransform = qrCorrespondence.sourceQR.transform;
            Transform targetQrTransform = qrCorrespondence.targetQR.transform;
            Matrix4x4 sourceMatrix = sourceQrTransform.GetMatrix();
            Matrix4x4 targetMatrix = targetQrTransform.GetMatrix();
            Matrix4x4 deltaPose = targetMatrix.inverse * sourceMatrix;
            float posMagnitudeSqr = deltaPose.GetPosition().sqrMagnitude;
            float rotationMagnitude = Quaternion.Angle(deltaPose.rotation, Quaternion.identity) / 180f * Mathf.PI;
            float error = posMagnitudeSqr * kPosWeight + rotationMagnitude * rotationMagnitude * kRotWeight;
            totalError += error;
        }

        return totalError;
    }
}

public static class LPPoseUtils {
    
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
