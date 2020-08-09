using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayFieldTest : MonoBehaviour
{
    #region properties

    [SerializeField]
    float factor = 1f; //, width = 3f, height = 2f;

    #endregion

    #region variables

    Camera mainCamera;
    Vector3 bottomLeft, bottomRight, topLeft, topRight, center, crosshair, camLocalPos;

    #endregion

    #region unity event functions

    private void Start()
    {
        mainCamera = Camera.main;
        camLocalPos = mainCamera.transform.localPosition;
    }

    private void Update()
    {
        factor = -mainCamera.transform.localPosition.z;
        float scrollWheel = Mouse.current.scroll.ReadValue().y;

        bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, factor));
        bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, factor));
        topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, factor));
        topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, factor));

        center = mainCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, factor));

        Vector2 mouseToViewport = new Vector2(  Mouse.current.position.ReadValue().x / Screen.width,
                                                Mouse.current.position.ReadValue().y / Screen.height);

        crosshair = mainCamera.ViewportToWorldPoint(new Vector3(mouseToViewport.x, mouseToViewport.y, factor));

        if (Keyboard.current.zKey.ReadValue() > 0 && Application.isFocused)
            camLocalPos.z += scrollWheel * .003f;

        mainCamera.transform.localPosition = camLocalPos;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(bottomRight, topRight);

            DebugGizmos.DrawX(center, .1f * factor, mainCamera.transform, Color.red);
            DebugGizmos.DrawX(crosshair, .2f * factor, mainCamera.transform, Color.yellow);

            Vector3 cameraToCrosshair = (crosshair - mainCamera.transform.position).normalized;

            Gizmos.DrawRay(crosshair, cameraToCrosshair * 10);
        }
    }

    #endregion

    #region methods



    #endregion
}
