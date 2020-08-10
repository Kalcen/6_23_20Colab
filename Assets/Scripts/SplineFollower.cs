using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class SplineFollower : MonoBehaviour
{
    #region properties

    public Spline spline;

    #endregion

    #region variables

    float locationOnSpline = 0f;

    #endregion

    #region unity event functions

    private void OnEnable()
    {
        if (spline != null)
        {
            transform.localPosition = spline.nodes[0].Position;
        }
    }

    private void Update()
    {
        PlaceOnSpline();
    }

    #endregion

    #region methods

    void PlaceOnSpline()
    {
        if (locationOnSpline < 0)
            locationOnSpline += spline.Length;
        locationOnSpline %= spline.Length;

        CurveSample sample = spline.GetSampleAtDistance(locationOnSpline);

        transform.localPosition = sample.location;
        transform.localRotation = sample.Rotation;
    }

    public void MoveOnSpline(float pos)
    {
        locationOnSpline = pos;
    }

    #endregion
}
