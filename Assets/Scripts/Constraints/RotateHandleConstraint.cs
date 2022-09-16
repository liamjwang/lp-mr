// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Constraints
{
    /// <summary>
    /// Component for fixing the rotation of a manipulated object relative to the world
    /// </summary>
    public class RotateHandleConstraint : TransformConstraint
    {
        #region Properties

        public override TransformFlags ConstraintType => TransformFlags.Rotate;

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Fix rotation to the rotation from manipulation start
        /// </summary>
        public override void ApplyConstraint(ref MixedRealityTransform transform)
        {
            transform.Position = Vector3.zero;
            Debug.Log(transform.Position);
            // transform.Position = worldPoseOnManipulationStart.Position;
        }

        #endregion Public Methods
    }
}