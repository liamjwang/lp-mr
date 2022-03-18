using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectsTo : MonoBehaviour
{
    
    public GameObject[] objects;
    public Transform target;
    public Vector3 offset;
    
    public void MoveObjects()
    {
        foreach (GameObject obj in objects)
        {
            obj.transform.position = target.position + offset;
        }
    }
}
