using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    private SplineContainer slope = null;

    float velocity;
    float acceleration;
    float mass;
    Vector3 normal;
    Vector3 unitNormal;
    float normalForce;
    float gravity = 9.81f;
    float gravitationalForce;
    float sumOfAllForce;

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

        gravitationalForce = -gravity * mass;
        // First find the normal in the current point, then do the following:
            
        Vector3 slopeUp = slope.Spline.EvaluateUpVector(t);
        unitNormal = slopeUp.normalized;
        normalForce = - Vector3.Dot(gravitationalForce * unitNormal, unitNormal);

        sumOfAllForce = gravitationalForce + normalForce;
        acceleration = sumOfAllForce / mass;
        velocity += acceleration;
        Debug.Log("Acceleration: " + acceleration + " | Velocity: " + velocity);

        GetComponent<Rigidbody>().AddForce(transform.forward * velocity);
    }
}
