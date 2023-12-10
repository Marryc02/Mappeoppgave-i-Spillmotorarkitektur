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

    private float slopeAngle = 15.0f;
    private float slopeLength = 150.0f;

    void Awake() 
    {
        if (SplineRef != null)
        {
            knots = SplineRef.Spline.ToArray().ToList();
            if (knots.Count != 0)
            {
                /*
                for (int i = 0; i < knots.Count; i++)
                {
                    Debug.Log("Knot " + (i + 1) + ": " + knots[i].Position);
                }
                */
                MoveKnots();
            }
        }
    }

    void MoveKnots()
    {
        float x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad);
        float y = Mathf.Sin(slopeAngle * Mathf.Deg2Rad);

        // x here is negative, since we're going in the negative x-axis in-game
        Vector3 p = new Vector3(-x, y, 0) * slopeLength;

        var knot1 = knots[2];
        knot1.Position = SplineRef.transform.InverseTransformDirection(p);
        SplineRef.Spline.SetKnot(2, knot1);

        var knot2 = knots[3];
        knot2.Position = SplineRef.transform.InverseTransformDirection(p + new Vector3(-40.0f, 0.0f, 0.0f));

        SplineRef.Spline.SetKnot(3, knot2);
    }
}
