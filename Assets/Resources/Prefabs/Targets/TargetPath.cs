using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SplineMesh.Spline))]
public class TargetPath : MonoBehaviour
{
    [SerializeField]
    bool useScreenSpace;
    [SerializeField]
    bool loopTarget;

    private void LateUpdate()
    {
        if (useScreenSpace) 
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
