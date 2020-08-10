using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SplineMesh.Spline))]
public class TargetPath : MonoBehaviour
{
    [SerializeField]
    TransformSpace transformSpace;
    SplineMesh.Spline path;

    private void Start()
    {
        path = GetComponent<SplineMesh.Spline>();
    }

    private void LateUpdate()
    {
        if (transformSpace == TransformSpace.screenSpace) 
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
            transform.rotation = Camera.main.transform.rotation;
        }
    }
    enum TransformSpace
    {
        splineSpace,
        screenSpace,
        worldSpace
    }
}
