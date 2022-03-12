using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowOther : MonoBehaviour
{

    public Transform other;
    
    private Vector3 velocity = Vector3.zero;
    private float currVel = 0;
    public float translateSmoothTime = 0.3F;
    public float rotateSmoothTime = 0.3F;
    public bool followScale = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = transform;
        myTransform.position = Vector3.SmoothDamp(myTransform.position, other.position, ref velocity, translateSmoothTime);
        Quaternion transformRotation = myTransform.rotation;
        float delta = Quaternion.Angle(transformRotation, other.rotation);
        if (delta > 0f)
        {
            float t = Mathf.SmoothDampAngle(delta, 0.0f, ref currVel, rotateSmoothTime);
            t = 1.0f - (t / delta);
            myTransform.rotation = Quaternion.Slerp(transformRotation, other.rotation, t);
        }

        if (followScale)
            myTransform.localScale = other.localScale;
    }
}
