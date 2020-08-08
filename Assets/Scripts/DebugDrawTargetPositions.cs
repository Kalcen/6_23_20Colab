using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawTargetPositions : MonoBehaviour
{
    static Transform xform;
    static List<TargetTracker> targets = new List<TargetTracker>();

    private void LateUpdate()
    {
        xform = transform;
        foreach (TargetTracker tt in targets) 
        {
            if (tt.UIParticleSystem != null) 
            {
                tt.UIParticleSystem.transform.position = tt.target.viewportToWorldPos;
            }
        }
    }

    public void AddTarget(TargetManager target) 
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Particles/UI Particles/TrackedTarget"), Vector3.zero, Quaternion.identity, xform);
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        main.simulationSpace = ParticleSystemSimulationSpace.Custom;
        main.customSimulationSpace = xform;

        TargetTracker newTarget;
        newTarget.target = target;
        newTarget.UIParticleSystem = go;

        targets.Add(newTarget);
    }
    public void RemoveTarget(TargetManager target)
    {
        TargetTracker removeTT = new TargetTracker();
        GameObject removeGO = null;
        bool found = false;
        foreach(TargetTracker tt in targets)
        {
            if (tt.target == target) 
            {
                found = true;
                removeGO = tt.UIParticleSystem;
                removeTT = tt;
            }
        }
        if (removeGO) 
        {
            targets.Remove(removeTT);
            StartCoroutine("DestroyObject", removeGO);
        }
    }

    struct TargetTracker 
    {
        public TargetManager target;
        public GameObject UIParticleSystem;
    }

    IEnumerator DestroyObject(GameObject remove) 
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(remove);
    }
}
