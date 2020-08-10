using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager origional;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (origional == null)
        {
            origional = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
}
