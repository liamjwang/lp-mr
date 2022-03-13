// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// [RequireComponent(typeof(USImageServiceProvider))]
// public class USImageDestSwitcher : MonoBehaviour
// {
//     
//     public MeshRenderer[] renderers;
//     public int currentIndex = 0;
//     
//     private USImageServiceProvider imageServiceProvider;
//     
//     // Start is called before the first frame update
//     void Start()
//     {
//         
//         imageServiceProvider = GetComponent<USImageServiceProvider>();
//         
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         imageServiceProvider.TargetMeshRenderer = renderers[currentIndex];
//     }
// }
