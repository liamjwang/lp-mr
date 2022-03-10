using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using ManipulationEventData = Microsoft.MixedReality.OpenXR.ManipulationEventData;

public class SnapAdjustController : MonoBehaviour
{
    public ObjectManipulator manipulator;

    public Transform snapAdjustOrigin;
    public Transform snapAdjustTarget;
    public MeshRenderer meshRenderer;

    public float minLerpK = 0.5f;
    public float maxLerpK = 0f;
    public float moveLerpTimeK = 0.5f;
    public float twoHandSmoothK = 0.5f;

    public float snapDeadzone = 0.05f;

    public int snapSigFigs = -2;
    
    public List<LerpTimeLookup> lerpTimeLookups = new List<LerpTimeLookup>();
    
    [System.Serializable]
    public struct LerpTimeLookup
    {
        public int sigFigs;
        public float lerpTime;
    }

    public GameObject xAxisIndicator;
    public GameObject yAxisIndicator;
    public GameObject zAxisIndicator;

    private IMixedRealityPointer mixedRealityPointer;

    private Vector3 velocity;
    private Vector3 twoHandVelocity;

    public Vector3 continuousPosition;
    private Vector3 snapManipulationAxis;
    private int axisIdx;
    

    // private float InitialMoveLerpTime;
    // private float InitialScaleLerpTime;
    // private float InitialRotateLerpTime;

    private DefaultTransformSmoothingLogic defaultTransformSmoothingLogic = new DefaultTransformSmoothingLogic();

    private int greatestNumberOfHands = 0;

    public UnityEvent OnInteractionStarted = new UnityEvent();
    public UnityEvent OnInteractionEnded = new UnityEvent();
    public UnityEvent OnValueUpdated = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        if (manipulator == null)
        {
            manipulator = GetComponent<ObjectManipulator>();
        }

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        manipulator.OnManipulationStarted.AddListener(ManipulationStart);
        manipulator.OnManipulationEnded.AddListener(ManipulationEnd);

        // InitialMoveLerpTime = manipulator.MoveLerpTime;
        // InitialScaleLerpTime = manipulator.ScaleLerpTime;
        // InitialRotateLerpTime = manipulator.RotateLerpTime;

        xAxisIndicator.SetActive(false);
        yAxisIndicator.SetActive(false);
        zAxisIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var twoHandManip = manipulator.GetPropertyValue<bool>("IsTwoHandedManipulationEnabled");
        var oneHandManip = manipulator.GetPropertyValue<bool>("IsOneHandedManipulationEnabled");

        if (!twoHandManip && !oneHandManip)
        {
            xAxisIndicator.SetActive(false);
            yAxisIndicator.SetActive(false);
            zAxisIndicator.SetActive(false);
        }
        else
        {
            OnValueUpdated.Invoke();
        }

        if (oneHandManip && greatestNumberOfHands < 1)
        {
            greatestNumberOfHands = 1;
        }

        if (twoHandManip && greatestNumberOfHands < 2)
        {
            greatestNumberOfHands = 2;
        }

        // meshRenderer.material.color = Color.white;
        if (greatestNumberOfHands == 2)
        {
            float roundToNearest = Mathf.Pow(10, snapSigFigs);

            
            // continuousPosition = transform.position;
            continuousPosition = Vector3.SmoothDamp(continuousPosition, transform.position, ref twoHandVelocity, twoHandSmoothK);
            Vector3 localPosition = continuousPosition - snapAdjustOrigin.position;

            var snapVectorList = new List<float>
            {
                localPosition.x,
                localPosition.y,
                localPosition.z
            };

            snapVectorList = snapVectorList.Select(x => Mathf.Round(x / roundToNearest) * roundToNearest).ToList();

            Vector3 snapVector = new Vector3(
                snapVectorList[0],
                snapVectorList[1],
                snapVectorList[2]
            );
            snapAdjustTarget.position = snapAdjustOrigin.position + snapVector;
            
            
        }

        if (greatestNumberOfHands == 1)
        {
            Vector3 dragVector = transform.position - continuousPosition;
            float distance = dragVector.magnitude;
            if (distance > snapDeadzone && snapManipulationAxis == Vector3.zero)
            {
                var directions = new List<float>
                {
                    dragVector.x,
                    dragVector.y,
                    dragVector.z
                };

                var absValDirections = directions.Select(Mathf.Abs).ToList();
                axisIdx = absValDirections.IndexOf(absValDirections.Max());
                xAxisIndicator.SetActive(false);
                yAxisIndicator.SetActive(false);
                zAxisIndicator.SetActive(false);
                switch (axisIdx)
                {
                    case 0:
                        xAxisIndicator.SetActive(true);
                        break;
                    case 1:
                        yAxisIndicator.SetActive(true);
                        break;
                    case 2:
                        zAxisIndicator.SetActive(true);
                        break;
                }

                var zeroes = new List<float>
                {
                    0f,
                    0f,
                    0f
                };
                zeroes[axisIdx] = directions[axisIdx];

                var proj = new Vector3(
                    zeroes[0],
                    zeroes[1],
                    zeroes[2]
                );

                snapManipulationAxis = proj.normalized;
            }

            if (snapManipulationAxis != Vector3.zero)
            {
                float roundToNearest = Mathf.Pow(10, snapSigFigs);

                float coef = Mathf.Lerp(minLerpK, maxLerpK, Mathf.InverseLerp(0f, 1f, distance - snapDeadzone));

                float found = 0;
                
                foreach (LerpTimeLookup lerpTimeLookup in lerpTimeLookups)
                {
                    if (lerpTimeLookup.sigFigs == snapSigFigs)
                    {
                        found = lerpTimeLookup.lerpTime;
                        break;
                    }
                }
                float lerpTime = moveLerpTimeK * coef * found;
                // Vector3 goal = transform.position;
                var dragVectorAligned = Vector3.Dot(snapManipulationAxis, dragVector) * snapManipulationAxis;
                Vector3 goal = continuousPosition + dragVectorAligned;
                // continuousPosition = defaultTransformSmoothingLogic.SmoothPosition(continuousPosition, goal, lerpTime, Time.deltaTime);
                velocity = Vector3.zero;
                
                continuousPosition = Vector3.SmoothDamp(continuousPosition, goal, ref velocity, lerpTime);

                Vector3 localPosition = continuousPosition - snapAdjustOrigin.position;
                
                var snapVectorList = new List<float>
                {
                    localPosition.x,
                    localPosition.y,
                    localPosition.z
                };

                snapVectorList[axisIdx] = Mathf.Round(snapVectorList[axisIdx] / roundToNearest) * roundToNearest;
                

                Vector3 snapVector = new Vector3(
                    snapVectorList[0],
                    snapVectorList[1],
                    snapVectorList[2]
                );
                snapAdjustTarget.position = snapAdjustOrigin.position + snapVector;
            }
        }
    }

    public void ManipulationStart(Microsoft.MixedReality.Toolkit.UI.ManipulationEventData data)
    {
        mixedRealityPointer = data.Pointer;

        continuousPosition = snapAdjustTarget.position;
        
        OnInteractionStarted?.Invoke();
    }

    public void ManipulationEnd(Microsoft.MixedReality.Toolkit.UI.ManipulationEventData data)
    {
        mixedRealityPointer = null;

        transform.position = snapAdjustTarget.position;
        transform.rotation = snapAdjustTarget.rotation;

        continuousPosition = snapAdjustTarget.position;
        snapManipulationAxis = Vector3.zero;

        greatestNumberOfHands = 0;
        velocity = Vector3.zero;
        
        OnInteractionEnded?.Invoke();
    }
}