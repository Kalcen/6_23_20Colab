using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFieldTest : MonoBehaviour
{
    #region properties

    [SerializeField]
    float width = 3f, height = 2f, factor = 1f;

    #endregion

    #region variables

    #endregion

    #region unity event functions

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Vector3 offsetPos = pos + transform.up * height * factor;

        Vector3 bottomLeft, bottomRight, topLeft, topRight;

        bottomLeft = pos - transform.right * width / 2 * factor;
        bottomRight = pos + transform.right * width / 2 * factor;
        topLeft = offsetPos - transform.right * width / 2 * factor;
        topRight = offsetPos + transform.right * width / 2 * factor;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);
    }

    #endregion

    #region methods



    #endregion
}
