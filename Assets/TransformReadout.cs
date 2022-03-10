using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransformReadout : MonoBehaviour
{

    public Transform origin;
    public Transform target;
    public TextMeshPro text;
    
    // Start is called before the first frame update
    void Start()
    {
        if (text == null)
        {
            text = GetComponent<TextMeshPro>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (origin == null || target == null)
        {
            return;
        }
        
        var relativeVector = target.position - origin.position;

        text.text = "";
        text.text += "Position:\n";
        // add trailing zeros to 5 decimal places
        text.text += $"X: {relativeVector.x*1000,10:######0.000}mm\n";
        text.text += $"Y: {relativeVector.y*1000,10:######0.000}mm\n";
        text.text += $"Z: {relativeVector.z*1000,10:######0.000}mm\n";
    }
}