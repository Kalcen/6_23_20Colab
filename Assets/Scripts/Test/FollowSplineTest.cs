using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Spline))]
public class FollowSplineTest : MonoBehaviour
{
    #region properties

    [SerializeField]
    float speed = 5;

    public moveStyle followType;
    public GameObject follower;

    #endregion

    #region variables

    Controls controls;

    Spline spline;
    float locationOnSpline = 0f;

    #endregion

    #region unity event functions

    private void Awake() => controls = new Controls();

    private void OnEnable()
    {
        spline = GetComponent<Spline>();

        controls.Player.Enable();
    }

    private void OnDisable() => controls.Player.Disable();

    private void Start()
    {
        if (follower != null)
        {
            follower.transform.position = spline.nodes[0].Position;
        }
    }

    private void LateUpdate()
    {
        if (follower != null)
        {
            switch (followType)
            {
                case moveStyle.FolowOverTime:
                    PFConstSpeed();
                    break;
                case moveStyle.ZeroToOne:
                    PFZeroToOne();
                    break;
                case moveStyle.VelocityControl:
                    PFVelocityControl();
                    break;
                default:
                    PFConstSpeed();
                    break;
            }
        }
    }

    #endregion

    #region methods

    void PFConstSpeed()
    {
        locationOnSpline += Time.deltaTime * speed;
        locationOnSpline %= spline.Length;
        CurveSample sample = spline.GetSampleAtDistance(locationOnSpline);

        follower.transform.position = sample.location;
        follower.transform.localRotation = sample.Rotation;
    }

    void PFZeroToOne()
    {
        float playerInput = controls.Player.Forward.ReadValue<float>() - controls.Player.Reverse.ReadValue<float>();

        locationOnSpline = Mathf.Clamp(spline.Length, 0, 20) * playerInput;
        if (locationOnSpline < 0)
            locationOnSpline += spline.Length;

        CurveSample sample = spline.GetSampleAtDistance(locationOnSpline);

        follower.transform.position = sample.location;
        follower.transform.localRotation = sample.Rotation;
    }

    void PFVelocityControl()
    {
        float playerInput = controls.Player.Forward.ReadValue<float>() - controls.Player.Reverse.ReadValue<float>();

        locationOnSpline += playerInput * Time.deltaTime * speed;
        if (locationOnSpline < 0)
            locationOnSpline += spline.Length;
        locationOnSpline %= spline.Length;

        CurveSample sample = spline.GetSampleAtDistance(locationOnSpline);

        follower.transform.position = sample.location;
        follower.transform.localRotation = sample.Rotation;
    }

    #endregion 
}

public enum moveStyle
{
    FolowOverTime,
    ZeroToOne,
    VelocityControl
}
