using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SplineMesh;

public class PlayerController : MonoBehaviour
{
    #region properties & variables

    Controls controls;

    #endregion

    //--UNITY EVENT FUNCTIONS

    private void Awake() => controls = new Controls();

    private void OnEnable() => controls.Player.Enable();

    private void OnDisable() => controls.Player.Disable();

    //--METHODS


}
