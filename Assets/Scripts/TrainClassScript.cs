using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TrainClassScript : MonoBehaviour
{
    public float velocity;
    public float acceleration;
    public float givenMass;

    void Start() {
       GetComponent<Rigidbody>().mass = givenMass;
    }
}
