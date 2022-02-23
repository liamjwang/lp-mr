using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class ToggleSolver : MonoBehaviour
{
    public SolverHandler solver = null;
    
    // Start is called before the first frame update
    void Start()
    {
        if (solver == null)
        {
            solver = GetComponent<SolverHandler>();
        }
    }

    public void Reset()
    {
        // bool ogSmoothing = solver.Smoothing;
        // solver.Smoothing = false;
        // solver.SolverUpdate();
        // solver.UpdateWorkingToGoal();
        // solver.Smoothing = ogSmoothing;
    }
}
