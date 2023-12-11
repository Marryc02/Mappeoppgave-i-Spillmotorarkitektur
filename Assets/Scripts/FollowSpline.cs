using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    [SerializeField] SplineAnimate splineAnimate;
    
    private SplineContainer slope = null;

    float velocity = MainScript.mainInstance.trainStartingVelocity;
    float acceleration;
    float mass = MainScript.mainInstance.trainMass + (MainScript.mainInstance.wagonMass * MainScript.mainInstance.wagonAmount);
    Vector3 normal;
    Vector3 unitNormal;
    float normalForce;
    float gravity = 9.81f;
    float gravitationalForce;
    float sumOfAllForce;

    void Awake()
    {
        GameObject splineObject = GameObject.FindGameObjectWithTag("Slope");
        if (splineObject != null) { slope = splineObject.GetComponent<SplineContainer>(); }
        else { Debug.Log("GameObject containing spline not found!"); }

        if (slope != null)
        {
            splineAnimate.Container = slope;
            splineAnimate.MaxSpeed = velocity;
        }
        else
        {
            Debug.Log("GameObject could not find spline component!");
        }
    }
    void FixedUpdate()
    {
        float distance = SplineUtility.GetNearestPoint(slope.Spline, transform.position, out float3 nearest, out float t);
        Debug.Log("Distance: " + distance);
        transform.position = nearest;

        Vector3 forward = Vector3.Normalize(slope.EvaluateTangent(t));
        Vector3 up = slope.EvaluateUpVector(t);

        Vector3 uForward = new Vector3(0, 1, 0);
        Vector3 uUp = new Vector3(1, 0, 0);

        var rotation = Quaternion.Inverse(Quaternion.LookRotation(forward, up));

        gravitationalForce = gravity * mass;
        // First find the normal in the current point, then do the following:
            /*
                unitNormal = normal.Normalize();
                normalForce = -(gravitationalForce * unitNormal) * unitNormal;
            */
        //
        sumOfAllForce = gravitationalForce + normalForce;
        acceleration = sumOfAllForce / mass;

        splineAnimate.MaxSpeed += acceleration;
    }
}
