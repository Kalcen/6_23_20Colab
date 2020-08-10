using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

[RequireComponent(typeof(SplineFollower))]
public class FollowParentSpline : MonoBehaviour
{
    #region properties

    [SerializeField]
    float speed;

    #endregion

    #region variables

    SplineFollower splineFollower;
    float locationOnSpline = 0;

    #endregion

    #region unity event functions

    private void OnEnable()
    {
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.spline = GetComponentInParent<Spline>();
    }

    //private void Update()
    //{
    //    FollowOverTime();
    //}

    #endregion

    #region methods

    //void FollowOverTime()
    //{
    //    locationOnSpline += Time.deltaTime * speed;

    //    splineFollower.MoveOnSpline(locationOnSpline);
    //}

    #endregion
}
