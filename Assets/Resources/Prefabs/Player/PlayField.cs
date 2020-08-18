using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SplineMesh;

[RequireComponent(typeof(SplineFollower))]
public class PlayField : MonoBehaviour
{
    #region properties & variables

    //props
    public float speed = 10;

    [HideInInspector]
    public Vector3 bottomLeft, bottomRight, topLeft, topRight, mouseReticle;
    [HideInInspector]
    public float trackWidth;

    //vars
    Camera mainCamera;
    float factor;
    SplineFollower splineFollower;

    #endregion

    //--UNITY EVENT FUNCTIONS

    private void Start()
    {
        mainCamera = Camera.main;

        splineFollower = GetComponent<SplineFollower>();
    }

    private void Update()
    {
        splineFollower.FollowOverTime(speed);
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
            DefineField();
    }

    private void OnDrawGizmos()
    {
        if (mainCamera != null)
            DrawField();
    }

    //--METHODS

    private void DefineField()
    {
        factor = -mainCamera.transform.localPosition.z;

        bottomLeft =    mainCamera.ViewportToWorldPoint(new Vector3(0, 0, factor));
        bottomRight =   mainCamera.ViewportToWorldPoint(new Vector3(1, 0, factor));
        topLeft =       mainCamera.ViewportToWorldPoint(new Vector3(0, 1, factor));
        topRight =      mainCamera.ViewportToWorldPoint(new Vector3(1, 1, factor));

        Vector2 mouseToViewport = new Vector2(  Mouse.current.position.ReadValue().x / Screen.width,
                                                Mouse.current.position.ReadValue().y / Screen.height);

        mouseReticle = mainCamera.ViewportToWorldPoint(new Vector3(mouseToViewport.x, mouseToViewport.y, factor));
    }

    private void DrawField()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);

        DebugGizmos.DrawX(mouseReticle, .1f * factor, mainCamera.transform, Color.red, 0);
    }
}
