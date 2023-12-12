using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text forceText;
    [SerializeField] TMP_Text velText;
    [SerializeField] TMP_Text accText;

    public static StatsUpdater Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    
    public void UpdateForce(float n)
    {
        forceText.text = "Sum of forces: " +  n.ToString() + "N";
    }

    public void UpdateVelocity(float n)
    {
        velText.text = "Velocity: " + n.ToString() + "km/h";
    }

    public void UpdateAcceleration(float n)
    {
        accText.text = "Acceleration: " +  n.ToString() + "m/s^2";
    }
}
