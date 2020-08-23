using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SplineMesh;

[ExecuteInEditMode]
public class PlayerController : MonoBehaviour
{
    #region properties & variables
    Controls controls;
    Rigidbody body;
    [SerializeField]
    Spline spline;

    int contactCount;
    int groundContactCount;
    int wallContactCount;

    [SerializeField, Range(0f, 10f)]
    float maxHorizontalSpeed = 24;
    [SerializeField, Range(0f, 100f)]
    float maxSplineSpeed = 35;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 45f;
    [SerializeField, Range(0f, 90f)]
    float maxGroundAngle = 60;
    float minGroundDotProduct;
    float minWallDotProduct;


    Vector2 movementInput;

    Vector3 desiredVelocity;
    Vector3 velocity;
    Vector3 pointOnSpline;
    [SerializeField]
    Vector3 startingPointOnSpline;
    Vector3 contactNormal, groundNormal, wallNormal;
    #endregion

    //--UNITY EVENT FUNCTIONS
    private void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }
    private void Awake() => controls = new Controls();
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.sleepThreshold = 0;
        pointOnSpline = startingPointOnSpline;
    }
    private void OnEnable()
    {
        if (controls == null) 
        {
            controls = new Controls();
        }
        controls.Player.Enable();
        controls.Player.Movement.performed += ctx => OnMovement(ctx);
        controls.Player.Movement.canceled += ctx => OnMovement(ctx);
    }
    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Movement.performed -= ctx => OnMovement(ctx);
        controls.Player.Movement.canceled -= ctx => OnMovement(ctx);
    }
    private void Update()
    {
        desiredVelocity.x = movementInput.x * maxHorizontalSpeed;
    }
    private void FixedUpdate()
    {
        if (Application.isPlaying)
        {
            float maxSpeedChange = maxAcceleration * Time.deltaTime;
            UpdateSplineDistance();
            transform.rotation = spline.GetSampleAtDistance(pointOnSpline.x).Rotation;
            //Apply gravity
            if (groundContactCount > 0)
            {
                velocity.y = 0;
            }
            velocity.y += Physics.gravity.magnitude * Time.deltaTime;
            //apply movement input
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            if (wallContactCount > 0 && Vector3.Dot(transform.right * desiredVelocity.x, wallNormal) < 0)
            {
                velocity.x = 0;
            }
            pointOnSpline.z += velocity.x * Time.deltaTime;
            //Set spline location
            Vector3 target = spline.GetSampleAtDistance(pointOnSpline.x).location + (pointOnSpline.z * transform.right);
            target.y = body.position.y;
            body.MovePosition(target + (Physics.gravity.normalized * (velocity.y * Time.deltaTime)));
            ClearState();
        }
        else 
        {
            PlaceAtStartingPoint();
        }
    }

    //--METHODS
    void ClearState() 
    {
        contactCount = groundContactCount = wallContactCount = 0;
        contactNormal = groundNormal = wallNormal = Vector3.zero;
    }
    void PlaceAtStartingPoint() 
    {
        startingPointOnSpline.x = Mathf.Clamp(startingPointOnSpline.x, 0, spline.Length);

        transform.position = spline.GetSampleAtDistance(startingPointOnSpline.x).location + (transform.right * startingPointOnSpline.z) + (transform.up * startingPointOnSpline.y);
        transform.rotation = spline.GetSampleAtDistance(startingPointOnSpline.x).Rotation;
    }
    void UpdateSplineDistance() 
    {
        pointOnSpline.x += maxSplineSpeed * Time.deltaTime;
        if (pointOnSpline.x < 0)
            pointOnSpline.x += spline.Length;
        pointOnSpline.x %= spline.Length;
    }
    void AdjustVelocity() 
    {
        velocity += desiredVelocity;
    }

    //--INPUT
    void OnMovement(InputAction.CallbackContext ctx) 
    {
        switch (ctx.phase) 
        {
            case InputActionPhase.Performed:
                movementInput.x = ctx.ReadValue<Vector2>().x;
                break;
            case InputActionPhase.Canceled:
                movementInput.x = 0;
                break;
        }
    }

    //--COLLISION EVALUATION
    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
    }
    void EvaluateCollision(Collision collision) 
    {
        foreach (ContactPoint point in collision.contacts) 
        {
            Vector3 normal = point.normal;
            if (normal.y >= minGroundDotProduct)
            {
                groundContactCount++;
                contactCount++;
                groundNormal += normal;
            }
            //gotta put the decimal to avoid weird floating point bullshit
            else if (normal.y <= 0.1f && normal.y > -1)
            {
                wallContactCount++;
                contactCount++;
                wallNormal += normal;
            }
            else 
            {
                contactCount++;
                contactNormal += normal;
            }
        }
    }
}
