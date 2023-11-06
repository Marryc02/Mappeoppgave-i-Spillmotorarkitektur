using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonClassScript : MonoBehaviour
{
    [SerializeField] float givenMass; // MAKE SURE THERE IS A MAX CAPACITY OF 80 TONNES AND A MINIMUM CAPACITY OF 40 TONNES

    void Start() {
       GetComponent<Rigidbody>().mass = givenMass;
    }
}
