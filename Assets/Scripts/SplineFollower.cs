using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

[ExecuteInEditMode]
public class SplineFollower : MonoBehaviour
{
    #region properties

    [SerializeField]
    bool useParentSpline;
    public Spline spline;
    [SerializeField]
    float speed;

    #endregion

    #region variables

    [SerializeField]
    float locationOnSpline = 0f;

    #endregion

    #region unity event functions

    private void OnEnable()
    {
        if (useParentSpline) 
        {
            if (GetComponentInParent<Spline>())
            {
                transform.localPosition = GetComponentInParent<Spline>().nodes[0].Position;
            }
            else 
            {
                print("Cannot locate parent spline.");
            }
        }
        if (spline != null)
        {
            transform.position = spline.nodes[0].Position;
        }
    }

    private void Update()
    {
        //correct serialized variables in editor while game is not in play mode
        if (!Application.isPlaying) 
        {
            if (useParentSpline)
            {
                if (GetComponentInParent<Spline>())
                {
                    locationOnSpline = Mathf.Clamp(locationOnSpline, 0, GetComponentInParent<Spline>().Length);
                }
                else
                {
                    print("Cannot locate parent spline.");
                }
            }
            if (spline != null)
            {
                locationOnSpline = Mathf.Clamp(locationOnSpline, 0, spline.Length);
            }
        }
        //execute in play mode
        if (useParentSpline)
        {
            if (GetComponentInParent<Spline>())
            {
                PlaceOnSpline(GetComponentInParent<Spline>());
            }
            else
            {
                print("Cannot locate parent spline.");
            }
        }
        else
        {
            PlaceOnSpline(spline);
        }
        FollowOverTime();
    }

    #endregion

    #region methods

    void PlaceOnSpline(Spline spline)
    {
        if (locationOnSpline < 0)
            locationOnSpline += spline.Length;
        locationOnSpline %= spline.Length;

        CurveSample sample = spline.GetSampleAtDistance(locationOnSpline);

        if (useParentSpline)
        {
            transform.localPosition = sample.location;
            transform.localRotation = sample.Rotation;
        }
        else 
        {
            transform.position = sample.location;
            transform.rotation = sample.Rotation;
        }
    }

    void FollowOverTime()
    {
        locationOnSpline += Time.deltaTime * speed;
    }

    #endregion
}
