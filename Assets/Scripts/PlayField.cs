using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SplineMesh;

[RequireComponent(typeof(SplineFollower))]
public class PlayField : MonoBehaviour
{
    #region properties

    //[SerializeField]
    Camera mainCamera;
    [SerializeField]
    float speed = 10;

    #endregion

    #region variables

    Controls controls;

    Vector3 bottomLeft, bottomRight, topLeft, topRight, reticle;
    float factor;

    SplineFollower splineFollower;
    float locationOnSpline = 0f;

    #endregion

    #region unity event functions

    private void Awake() => controls = new Controls();

    private void OnEnable() => controls.Player.Enable();

    private void OnDisable() => controls.Player.Disable();

    private void Start()
    {
        mainCamera = Camera.main;

        splineFollower = GetComponent<SplineFollower>();
    }

    private void Update()
    {
        splineFollower.FollowOverTime(speed);
        if (mainCamera != null)
            DefineField();
    }

    private void OnDrawGizmos()
    {
        if (mainCamera != null)
            DrawField();
    }

    #endregion

    #region methods

    private void MoveAlongSpline()
    {
        locationOnSpline += Time.deltaTime * speed;

        //splineFollower.MoveOnSpline(locationOnSpline);
    }

    private void DefineField()
    {
        factor = -mainCamera.transform.localPosition.z;

        bottomLeft =    mainCamera.ViewportToWorldPoint(new Vector3(0, 0, factor));
        bottomRight =   mainCamera.ViewportToWorldPoint(new Vector3(1, 0, factor));
        topLeft =       mainCamera.ViewportToWorldPoint(new Vector3(0, 1, factor));
        topRight =      mainCamera.ViewportToWorldPoint(new Vector3(1, 1, factor));

        Vector2 mouseToViewport = new Vector2(  Mouse.current.position.ReadValue().x / Screen.width,
                                                Mouse.current.position.ReadValue().y / Screen.height);

        reticle = mainCamera.ViewportToWorldPoint(new Vector3(mouseToViewport.x, mouseToViewport.y, factor));
    }

    private void DrawField()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);

        DebugGizmos.DrawX(reticle, .1f * factor, mainCamera.transform, Color.red, 0);
    }

    #endregion
}
