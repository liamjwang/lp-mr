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
    public float lr = 0.02f;
    public float lrDelta = 0.01f;
    public float lrMin = 0.0001f;
    public double minImprovement = 0.0001;
    public double excludeRotThreshold = 0.01;
    public double excludePosThreshold = 0.01;
    
    public double errorDisplay;

    private DateTimeOffset lastUpdate;


    void Start()
    {
        
    }

    void LateUpdate()
    {
        int latestCorrespondenceIndex = -1;
        DateTimeOffset latestCorrespondenceTime = DateTimeOffset.MinValue;
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
            transform.SetMatrix(desiredPose);
            
            recentCorrespondence.excluded = false;
            
            for (int i = 0; i < qrCorrespondences.Count; i++)
            {
                QRCorrespondence qrCorrespondence = qrCorrespondences[i];
                Transform sourceQrTransform = qrCorrespondence.sourceQR.transform;
                Transform targetQrTransform = qrCorrespondence.targetQR.transform;
                Matrix4x4 sourceMatrix = sourceQrTransform.GetMatrix();
                Matrix4x4 targetMatrix = targetQrTransform.GetMatrix();
                Matrix4x4 deltaPose = targetMatrix.inverse * sourceMatrix;
                double posMagnitude = deltaPose.GetPosition().magnitude;
                double rotationMagnitude = Quaternion.Angle(deltaPose.rotation, Quaternion.identity) / 180f * Mathf.PI;
                qrCorrespondence.excluded = posMagnitude > excludePosThreshold || rotationMagnitude > excludeRotThreshold;
                Debug.Log($"Excluded {i}? {posMagnitude > excludePosThreshold} {rotationMagnitude > excludeRotThreshold} {qrCorrespondence.excluded}");
            }
        }

        double frameInitialLoss = Loss();
        Matrix4x4 ogPose = transform.GetMatrix();
        for (int i = 0; i < 1000; i++)
        {
            StepOptimizer();
        }
        double afterFrameLoss = Loss();
        if (afterFrameLoss + minImprovement >= frameInitialLoss)
        {
            transform.SetMatrix(ogPose);
        }
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
        // Dumb optimizer that walks randomly instead of using gradient descent
        Quaternion rotationUniform = Random.rotationUniform;

        double ogError = Loss();

        Quaternion quaternion = Quaternion.Slerp(Quaternion.identity, rotationUniform, lr * 10);
        Matrix4x4 randomPose = Matrix4x4.TRS(Random.insideUnitSphere * lr, quaternion, new Vector3(1, 1, 1));
        Matrix4x4 ogPose = transform.GetMatrix();
        transform.SetMatrix(randomPose * ogPose);

        double newError = Loss();

        errorDisplay = ogError;

        if (newError >= ogError)
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

    private double Loss()
    {
        double totalError = 0;
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
            double posMagnitudeSqr = deltaPose.GetPosition().sqrMagnitude;
            double rotationMagnitude = Quaternion.Angle(deltaPose.rotation, Quaternion.identity) / 180f * Mathf.PI;
            double error = posMagnitudeSqr * kPosWeight + rotationMagnitude * rotationMagnitude * kRotWeight;
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
