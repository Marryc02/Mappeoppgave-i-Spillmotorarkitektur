using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    private SplineContainer slope = null;

    
    float mass;
    float gravity = 9.81f;
    float gravitationalForce;
    Vector3 normal;
    Vector3 unitNormal;
    float normalForce;
    // The coefficient of friction in our simulation is 0.6 as that number roughly represents the coeffcient of friction between to steel srufaces being rubbed against eachother.
    float frictionCoefficient = 0.6f;
    float friction;
    float sumOfAllForce;
    float velocity;
    float acceleration;

    public float spawnOffset = 0.0f;

    void Awake()
    {
        velocity = MainScript.mainInstance.trainStartingVelocity;
        mass = MainScript.mainInstance.trainMass + (MainScript.mainInstance.wagonMass * MainScript.mainInstance.wagonAmount);

        GameObject splineObject = GameObject.FindGameObjectWithTag("Slope");
        if (splineObject != null) { slope = splineObject.GetComponent<SplineContainer>(); }
        else { Debug.Log("GameObject containing spline not found!"); }

        if (slope == null)
        {
            Debug.Log("GameObject could not find spline component!");
        }        
    }
    
    void FixedUpdate()
    {
        var native = new NativeSpline(slope.Spline);
        float distance = SplineUtility.GetNearestPoint(native, transform.position, out float3 nearest, out float t);
        transform.position = nearest;
        Debug.Log("Nearest: " + nearest);
        Vector3 forward = Vector3.Normalize(slope.EvaluateTangent(t));
        Vector3 up = slope.EvaluateUpVector(t);

        Vector3 uForward = Vector3.forward;
        Vector3 uUp = Vector3.up;

        var rotation = Quaternion.Inverse(Quaternion.LookRotation(uForward, uUp));
        transform.rotation = Quaternion.LookRotation(forward, up) * rotation;

        // Gets G, aka gravitational force.
        gravitationalForce = -gravity * mass;
        
        // Finds N, aka normal force.
        Vector3 slopeUp = slope.Spline.EvaluateUpVector(t);
        unitNormal = slopeUp.normalized;
        normalForce = Vector3.Dot(-(gravitationalForce * unitNormal), unitNormal);

        // Finds F, aka friction.
        friction = frictionCoefficient * normalForce;

        // Adds all forces together and calculate a new speed based on them.
        sumOfAllForce = gravitationalForce + normalForce + friction;
        acceleration = sumOfAllForce / mass;
        velocity += acceleration;
        Debug.Log("Acceleration: " + acceleration + " | Velocity: " + velocity);

        GetComponent<Rigidbody>().AddForce(transform.forward * velocity);
    }
}
