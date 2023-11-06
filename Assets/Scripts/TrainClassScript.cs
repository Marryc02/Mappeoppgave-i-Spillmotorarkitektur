using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TrainClassScript : MonoBehaviour
{
    public TrainClassScript trainClassInstance;

    [SerializeField] float velocity;
    [SerializeField] float acceleration;
    [SerializeField] float givenMass;

    void Start() {
       GetComponent<Rigidbody>().mass = givenMass;
    }
}
