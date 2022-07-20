using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiQRTrack : MonoBehaviour
{

    [Serializable]
    public class QRCorrespondence
    {
        public QRFollower targetQR;
        public GameObject sourceQR;
        
        [HideInInspector]
        public bool excluded;
    }
    
    public List<QRCorrespondence> qrCorrespondences;
    public float kPosWeight = 1;
    public float kRotWeight = 1;
    public float translateLr = 0.02f;
    public float rotateLr = 0.02f;
    public float translateSlopeStep = 0.001f;
    public float rotateSlopeStep = 0.001f;
    public float minImprovement = 0.0001f;
    public float excludeRotThreshold = 0.01f;
    public float excludePosThreshold = 0.01f;
    
    public float errorDisplay;

    private long lastUpdate;


    void Start()
    {
        
    }

    void LateUpdate()
    {
        int latestCorrespondenceIndex = -1;
        long latestCorrespondenceTime = 0;
        for (int i = 0; i < qrCorrespondences.Count; i++)
        {
            QRCorrespondence corr = qrCorrespondences[i];
            if (corr.targetQR.lastUpdated > latestCorrespondenceTime)
            {
                latestCorrespondenceTime = corr.targetQR.lastUpdated;
                latestCorrespondenceIndex = i;
            }
        }

        if (lastUpdate < latestCorrespondenceTime)
        {
            lastUpdate = latestCorrespondenceTime;
            Debug.Log($"Latest correspondence is {latestCorrespondenceIndex}");
            QRCorrespondence recentCorrespondence = qrCorrespondences[latestCorrespondenceIndex];
            Matrix4x4 desiredPose = CalculateDesiredPose(recentCorrespondence);
            // transform.SetMatrix(desiredPose);
            
            recentCorrespondence.excluded = false;
            
            for (int i = 0; i < qrCorrespondences.Count; i++)
            {
                QRCorrespondence qrCorrespondence = qrCorrespondences[i];
                Transform sourceQrTransform = qrCorrespondence.sourceQR.transform;
                Transform targetQrTransform = qrCorrespondence.targetQR.transform;
                Matrix4x4 sourceMatrix = sourceQrTransform.GetMatrix();
                Matrix4x4 targetMatrix = targetQrTransform.GetMatrix();
                Matrix4x4 deltaPose = targetMatrix.inverse * sourceMatrix;
                float posMagnitude = deltaPose.GetPosition().magnitude;
                float rotationMagnitude = Quaternion.Angle(deltaPose.rotation, Quaternion.identity) / 180f * Mathf.PI;
                qrCorrespondence.excluded = posMagnitude > excludePosThreshold || rotationMagnitude > excludeRotThreshold;
                Debug.Log($"Excluded {i}? {posMagnitude > excludePosThreshold} {rotationMagnitude > excludeRotThreshold} {qrCorrespondence.excluded}");
            }
        }

        // float frameInitialLoss = Loss();
        // Matrix4x4 ogPose = transform.GetMatrix();
        for (int i = 0; i < 100; i++)
        {
            StepOptimizer();
        }
        // float afterFrameLoss = Loss();
        // if (afterFrameLoss + minImprovement >= frameInitialLoss)
        // {
        //     transform.SetMatrix(ogPose);
        // }
    }

    private static Matrix4x4 CalculateDesiredPose(QRCorrespondence qrCorrespondence)
    {
        Transform sourceQrTransform = qrCorrespondence.sourceQR.transform;
        Transform targetQrTransform = qrCorrespondence.targetQR.transform;
        Matrix4x4 sourceMatrix = sourceQrTransform.GetMatrix(Space.Self);
        Matrix4x4 targetMatrix = targetQrTransform.GetMatrix(Space.World);
        Matrix4x4 desiredPose = targetMatrix * sourceMatrix.inverse;
        return desiredPose;
    }

    private void StepOptimizer()
    {
        float ogError = Loss();

        Vector3[] directions = new Vector3[]
        {
            Vector3.forward,
            Vector3.right,
            Vector3.up,
        };

        foreach (Vector3 direction in directions)
        {
            Matrix4x4 slopeStep = Matrix4x4.TRS(direction * translateSlopeStep, Quaternion.identity, new Vector3(1, 1, 1));
            Matrix4x4 ogPose = transform.GetMatrix();
            transform.SetMatrix(slopeStep * ogPose);

            float newError = Loss();
            float errorSlope = (newError - ogError) / translateSlopeStep;
        
            Matrix4x4 moveStep = Matrix4x4.TRS(direction * (-errorSlope * translateLr), Quaternion.identity, new Vector3(1, 1, 1));
            transform.SetMatrix(moveStep * ogPose);
        }
        
        
        foreach (Vector3 direction in directions)
        {
            Matrix4x4 slopeStep = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(direction * rotateSlopeStep), new Vector3(1, 1, 1));
            Matrix4x4 ogPose = transform.GetMatrix();
            transform.SetMatrix(slopeStep * ogPose);

            float newError = Loss();
            float errorSlope = (newError - ogError) / translateSlopeStep;
        
            Matrix4x4 moveStep = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(direction * (-errorSlope * rotateLr)), new Vector3(1, 1, 1));
            transform.SetMatrix(moveStep * ogPose);
        }


        //
        // if (newError >= ogError)
        // {
        //     transform.SetMatrix(ogPose);
        //     lr *= 1 - lrDelta;
        // }
        // else
        // {
        //     lr *= 1 + lrDelta;
        // }
        //
        // if (lr < lrMin)
        // {
        //     lr = lrMin;
        // }
    }

    private float Loss()
    {
        float totalError = 0;
        foreach (QRCorrespondence qrCorrespondence in qrCorrespondences)
        {
            if (qrCorrespondence.excluded)
            {
                continue;
            }
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
