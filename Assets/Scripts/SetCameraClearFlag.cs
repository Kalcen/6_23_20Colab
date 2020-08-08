using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class SetCameraClearFlag : MonoBehaviour
{
    [SerializeField]
    CameraClearFlags camClearFlags;

    Camera cam;

    private void OnValidate()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = camClearFlags;
    }
}
