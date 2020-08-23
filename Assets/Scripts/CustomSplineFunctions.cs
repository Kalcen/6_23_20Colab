using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomSplineFunctions
{
    public static float GetDistanceFromSample(SplineMesh.Spline spline, SplineMesh.CurveSample sample) 
    {
        float cumulatedDistance = 0;
        foreach (SplineMesh.CubicBezierCurve curve in spline.curves) 
        {
            if (sample.curve != curve)
            {
                cumulatedDistance += curve.Length;
            }
            else 
            {
                cumulatedDistance += sample.distanceInCurve;
                return cumulatedDistance;
            }
        }
        return cumulatedDistance;
    }
}
