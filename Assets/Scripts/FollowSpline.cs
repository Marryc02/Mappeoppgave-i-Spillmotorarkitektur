using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    [SerializeField] SplineAnimate splineAnimate;

    void Awake()
    {
        GameObject splineObject = GameObject.FindGameObjectWithTag("Slope");
        SplineContainer slope = null;
        if (splineObject != null) { slope = splineObject.GetComponent<SplineContainer>(); }
        else { Debug.Log("GameObject containing spline not found!"); }

        if (slope != null)
        {
            splineAnimate.Container = slope;
        }
        else
        {
            Debug.Log("GameObject could not find spline component!");
        }
    }
}
