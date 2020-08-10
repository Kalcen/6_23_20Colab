using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TargetMover : MonoBehaviour
{
    //movement
    
    //debug values
    [SerializeField]
    bool debug;
    [HideInInspector]
    public Vector3 viewportToWorldPos;
    bool inView;
    [SerializeField]
    LayerMask inViewDebugLayerMask;

    private void LateUpdate()
    {
        if (Application.isPlaying)
        {
            if (debug)
            {
                DebugDrawTargetOnScreen();
            }
        }
    }

    void DebugDrawTargetOnScreen()
    {
        if (Camera.main.transform.parent.GetComponentInChildren<DebugDrawTargetPositions>())
        {
            Vector3 targetToViewportPos = Camera.main.WorldToViewportPoint(GetComponent<Collider>().bounds.center);
            if (targetToViewportPos.x > 0 && targetToViewportPos.x < 1 && targetToViewportPos.y > 0 && targetToViewportPos.y < 1 && targetToViewportPos.z > 0)
            {
                viewportToWorldPos = Camera.main.ViewportToWorldPoint(Vector3.Scale(targetToViewportPos, new Vector3(1, 1, 0)) + new Vector3(0, 0, 3));
                Ray ray = new Ray(GetComponent<Collider>().bounds.center, (viewportToWorldPos - GetComponent<Collider>().bounds.center).normalized);
                if (!Physics.Raycast(ray, targetToViewportPos.z, inViewDebugLayerMask, QueryTriggerInteraction.Ignore))
                {
                    if (!inView)
                    {
                        Camera.main.transform.parent.GetComponentInChildren<DebugDrawTargetPositions>().AddTarget(this);
                        inView = true;
                    }
                }
                else
                {
                    if (inView)
                    {
                        print("remove");
                        viewportToWorldPos = transform.position;
                        Camera.main.transform.parent.GetComponentInChildren<DebugDrawTargetPositions>().RemoveTarget(this);
                        inView = false;
                    }
                }
            }
            else
            {
                if (inView)
                {
                    viewportToWorldPos = transform.position;
                    Camera.main.transform.parent.GetComponentInChildren<DebugDrawTargetPositions>().RemoveTarget(this);
                    inView = false;
                }
            }
        }
    }
}
