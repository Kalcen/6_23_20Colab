using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{

    private static SceneSwitcher origional;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1)) 
            {
                SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            {
                SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
            }
        }
    }
}
