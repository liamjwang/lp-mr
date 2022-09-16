// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using Util;

namespace Microsoft.MixedReality.Toolkit.UI
{
    /// <summary>
    /// Component for limiting the translation axes for ObjectManipulator
    /// or BoundsControl
    /// </summary>
    public class FrameLocalRotateConstraint : TransformConstraint
    {
        #region Properties

        public ObjectManipulator objectManipulator;
        public float deadzoneRadius = 0.02f;

        [SerializeField]
        [EnumFlags]
        [Tooltip("Constrain movement along an axis")]
        private AxisFlags constraintOnMovement = 0;

        /// <summary>
        /// Constrain movement along an axis
        /// </summary>
        public AxisFlags ConstraintOnMovement
        {
            get => constraintOnMovement;
            set => constraintOnMovement = value;
        }

        [SerializeField]
        [Tooltip("Relative to rotation at manipulation start or world")]
        private bool useLocalSpaceForConstraint = false;

        private IMixedRealityPointer dragPointer;
        private Matrix4x4 pointerInitialPose;
        public float deadzoneB = 0.05f;
        private bool disableConstraintPressed;
        public Handedness applyToHand = Handedness.Left;
        public InputSourceType applyToInputSourceType = InputSourceType.Controller;

        /// <summary>
        /// Relative to rotation at manipulation start or world
        /// </summary>
        public bool UseLocalSpaceForConstraint
        {
            get => useLocalSpaceForConstraint;
            set => useLocalSpaceForConstraint = value;
        }

        public override TransformFlags ConstraintType => TransformFlags.Move;

        #endregion Properties

        #region Public Methods

        private void Start()
        {
            objectManipulator.OnManipulationStarted.AddListener(OnManipulationStarted);
            objectManipulator.OnManipulationEnded.AddListener(OnManipulationEnded);
        }


        private void OnManipulationStarted(ManipulationEventData arg0)
        {
            dragPointer = arg0.Pointer;
            pointerInitialPose = Matrix4x4.TRS(dragPointer.Position, dragPointer.Rotation, Vector3.one);
            disableConstraintPressed = false;
            
            if ((!Application.isEditor && arg0.Pointer.Controller.InputSource.SourceType != applyToInputSourceType) || arg0.Pointer.Controller.ControllerHandedness != applyToHand)
            {
                disableConstraintPressed = true;
            }
        }
        
        private void OnManipulationEnded(ManipulationEventData arg0)
        {
        }

        /// <summary>
        /// Removes movement along a given axis if its flag is found
        /// in ConstraintOnMovement
        /// </summary>
        public override void ApplyConstraint(ref MixedRealityTransform transform)
        {
            if (disableConstraintPressed)
            {
                return;
            }
            // Debug.Log("ApplyConstraint");
            foreach (MixedRealityInteractionMapping mixedRealityInteractionMapping in dragPointer.Controller.Interactions)
            {
                if (mixedRealityInteractionMapping.Description == "Axis1D.SecondaryIndexTrigger Press" || mixedRealityInteractionMapping.Description == "Axis1D.PrimaryIndexTrigger Press")
                {
                    if (mixedRealityInteractionMapping.BoolData)
                    {
                        disableConstraintPressed = true;
                        return;
                    }
                }
            }
            transform.Position = worldPoseOnManipulationStart.Position;
            Quaternion deltaRot = Quaternion.Inverse(worldPoseOnManipulationStart.Rotation) * transform.Rotation;
            deltaRot.ToAngleAxis(out float angle, out Vector3 axis);
            List<float> xyz = new List<float> { axis.x, axis.y, axis.z };
            int maxIndex = -1;
            float maxValue = -1;
            for (int i = 0; i < xyz.Count; i++)
            {
                if (Mathf.Abs(xyz[i]) > maxValue)
                {
                    maxValue = Mathf.Abs(xyz[i]);
                    maxIndex = i;
                }
            }
            for (int i = 0; i < xyz.Count; i++)
            {
                if (i != maxIndex)
                {
                    xyz[i] = 0;
                }
            }
            axis = new Vector3(xyz[0], xyz[1], xyz[2]);
            if (angle > 30f)
            {
                angle = 90f;
            }
            else
            {
                angle = 0;
            }
            transform.Rotation = worldPoseOnManipulationStart.Rotation * Quaternion.AngleAxis(angle, axis);
        }

        #endregion Public Methods
    }
}