using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    [SerializeField] SplineContainer SplineRef;

    List<BezierKnot> knots;

    private float slopeAngle = 5.0f;
    private float slopeLength = 50.0f;

    void Awake() 
    {
        if (SplineRef != null)
        {
            knots = SplineRef.Spline.ToArray().ToList();
            if (knots.Count != 0)
            {
                for (int i = 0; i < knots.Count; i++)
                {
                    Debug.Log("Knot " + (i + 1) + ": " + knots[i].Position);
                }
                MoveKnots();
            }
        }
    }

    void MoveKnots()
    {
        float x = Mathf.Sin(slopeAngle);
        float y = Mathf.Cos(slopeAngle);
        Vector3 p = new Vector3(x, y, 0) * slopeLength;
        var knot = knots[2];
        knot.Position = SplineRef.transform.InverseTransformDirection(p);
        SplineRef.Spline.SetKnot(2, knot);
        knot = knots[3];
        knot.Position = knots[2].Position + new float3(-40.0f, 0.0f, 0.0f);
        SplineRef.Spline.SetKnot(3, knot);
    }
}
