using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugGizmos
{
    //pos:          the world position at which to draw the shape
    //radius:       radius of the shape :|
    //orientation:  what x and y axis is the shape drawn relative to?
    //color:        colorrrr
    //rot:          the rotation of the shape on the z axis
    public static void DrawX(Vector3 pos, float radius, Transform orientation, Color color, float rot = 0)
    {
        float increment = Mathf.PI / 2;
        float rotRadians = (rot + 45) * Mathf.Deg2Rad;

        for (int i = 0; i < 2; i++)
        {
            Vector3 lineStart, lineEnd;

            lineStart =     (orientation.right *    Mathf.Sin(i * increment + rotRadians) * radius) +
                            (orientation.up *       Mathf.Cos(i * increment + rotRadians) * radius);
            lineEnd =       (orientation.right *    Mathf.Sin((i + 2) * increment + rotRadians) * radius +
                            (orientation.up *       Mathf.Cos((i + 2) * increment + rotRadians) * radius));

            Gizmos.color = color;
            Gizmos.DrawLine(lineStart + pos, lineEnd + pos);
        }
    }
}
