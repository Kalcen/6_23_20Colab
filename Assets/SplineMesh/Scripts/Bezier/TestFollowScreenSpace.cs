using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestFollowScreenSpace : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0,0,10));
        transform.rotation = Camera.main.transform.rotation;
    }
}
