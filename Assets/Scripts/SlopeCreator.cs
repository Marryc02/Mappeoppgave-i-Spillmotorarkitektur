using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class SlopeCreator : MonoBehaviour
{
    [SerializeField] SplineContainer SplineRef;

    [SerializeField] Canvas UIRef;

    UIScript UIScriptRef;

    private bool bHasAdjusted = false;

    List<BezierKnot> knots;

    private float slopeAngle = 5.0f;
    private float slopeLength = 10.0f;

    void Awake() 
    {
        if (SplineRef != null)
        {
            knots = SplineRef.Spline.ToArray().ToList();
            /*if (knots.Count != 0)
            {
                for (int i = 0; i < knots.Count; i++)
                {
                    Debug.Log("Knot " + (i + 1) + ": " + knots[i].Position);
                }  
            }
            */
        }
    }

    void Start()
    {
        UIScriptRef = UIRef.GetComponent<UIScript>();
        if (UIScriptRef == null)
        {
            Debug.Log("UI Reference in FollowSpline returned null!");
        }
    }

    void MoveKnots()
    {
        // We start off by using the unit circle to find in which direction the point is
        float x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad);
        float y = Mathf.Sin(slopeAngle * Mathf.Deg2Rad);

        // x here is negative, since we're going in the negative x-axis in-game.
        // After we've found the x- and y-values we multiply with the slopeLength to
        // find the position of the new point.
        Vector3 p = new Vector3(-x, y, 0) * slopeLength;


        // This knot is at the top of the slope.
        var knot1 = knots[2];
        knot1.Position = SplineRef.transform.InverseTransformDirection(p);
        SplineRef.Spline.SetKnot(2, knot1);

        // This knot is here to create a flat surface behind the last knot
        var knot2 = knots[3];
        knot2.Position = SplineRef.transform.InverseTransformDirection(p + new Vector3(-40.0f, 0.0f, 0.0f));

        SplineRef.Spline.SetKnot(3, knot2);
    }

    void Update()
    {
        // For getting UI values and setting them to create the slope.
        if (bHasAdjusted == false)
        {
            // Now to check if the packet recieved from the UI is valid
            if (UIScriptRef.shippingPacket.validPacket == true)
            {
                // Assigning the values
                slopeAngle = UIScriptRef.shippingPacket.slopeAngle;
                slopeLength = UIScriptRef.shippingPacket.slopeLength;
                bHasAdjusted = true;
                MoveKnots();
            }
        }
    }
}
