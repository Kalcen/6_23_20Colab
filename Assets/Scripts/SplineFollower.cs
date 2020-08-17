using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

[ExecuteInEditMode]
public class SplineFollower : MonoBehaviour
{
    #region properties & variables
    
    //props
    public bool useParentSpline, autoFollow;

    public Spline spline;
    Spline parentSpline;

    [SerializeField]
    float speed;

    //vars
    public float locationOnSpline = 0f;

    #endregion

    //--UNITY EVENT FUNCTIONS

    private void OnEnable()
    {
        parentSpline = GetComponentInParent<Spline>();

        if (spline != null)
        {
            transform.position = spline.nodes[0].Position;
        }
    }

    private void Update()
    {
        if (spline != null)
        {
            PlaceOnSpline(useParentSpline ? parentSpline : spline);
            if (Application.isPlaying && autoFollow)
                FollowOverTime(speed);
        }
        else
        {
            Debug.LogError(string.Format("Cannot locate{0}spline.", useParentSpline ? " parent " : " "));
        }
    }

    //--METHODS

    public void PlaceOnSpline(Spline spline)
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
            transform.position = sample.location + spline.transform.position;
            transform.rotation = sample.Rotation;
        }
    }

    public void FollowOverTime(float speed)
    {
        locationOnSpline += Time.deltaTime * speed;
    }
}
