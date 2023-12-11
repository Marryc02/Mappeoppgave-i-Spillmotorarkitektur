using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    [SerializeField] SplineAnimate splineAnimate;

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
        SplineContainer slope = null;
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

    private void FixedUpdate() {
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
