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
    public Spline targetSpline;
    public Vector3 locationOnSpline = Vector3.zero;

    //vars
    Spline parentSpline;
    [SerializeField]
    float speed;

    #endregion

    //--UNITY EVENT FUNCTIONS

    private void OnEnable()
    {
        parentSpline = GetComponentInParent<Spline>();

        if (targetSpline && !useParentSpline)
            transform.position = targetSpline.nodes[0].Position;

        else if (parentSpline && useParentSpline)
            transform.localPosition = parentSpline.nodes[0].Position;

        else
            Debug.LogError(string.Format("Cannot locate{0}spline for {1}.", useParentSpline ? " parent " : " ", gameObject.name));
    }

    private void Update()
    {
        Spline spline = useParentSpline ? parentSpline : targetSpline;

        if (spline)
        {
            PlaceOnSpline(spline);
            if (Application.isPlaying && autoFollow)
                FollowOverTime(speed);
        }
        else
        {
            Debug.LogError(string.Format("Cannot locate{0}spline for {1}.", useParentSpline ? " parent " : " ", gameObject.name));
        }
    }

    //--METHODS

    public void PlaceOnSpline(Spline spline)
    {
        if (locationOnSpline.x < 0)
            locationOnSpline.x += spline.Length;
        locationOnSpline.x %= spline.Length;

        CurveSample sample = spline.GetSampleAtDistance(locationOnSpline.x);
        Vector3 location = sample.location + (transform.right * locationOnSpline.z) + (transform.up * locationOnSpline.y);

        if (useParentSpline)
        {
            transform.localPosition = location;
            transform.localRotation = sample.Rotation;
        }
        else 
        {
            transform.position = location + spline.transform.position;
            transform.rotation = sample.Rotation;
        }
    }

    public void FollowOverTime(float speed)
    {
        locationOnSpline.x += Time.deltaTime * speed;
    }
}
