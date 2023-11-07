using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject wagonPrefab;
    public GameObject smallTrainPrefab;
    public GameObject mediumTrainPrefab;
    public GameObject largeTrainPrefab;


    string trainType = "Small";
    Vector3 trainVelocity;
    Vector3 lastVelocity;
    Vector3 trainAcceleration;

    int wagonAmount = 3;
    float wagonMass;


    List<GameObject> trainAndWagons;


    void SpawnTrainAndWagons()
    {
        // Adds a prefab of either type "Small", "Medium" or "Large" to the trainAndWagons-List and assigfns a given velocity.
        if (trainType == "Small") {
            trainAndWagons.Add(smallTrainPrefab);
            trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainVelocity;
        }
        else if (trainType == "Medium") {
            trainAndWagons.Add(mediumTrainPrefab);
            trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainVelocity;
        }
        else if (trainType == "Large") {
            trainAndWagons.Add(largeTrainPrefab);
            trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainVelocity;
        }
        // Ensures an actual train type is selected.
        else {
            Debug.Log("No train type selected!");
        }

        // Adds wagons based on the specified amount of wagons the user wants to add to the trainAndWagons-List.
        for (int i = 0; i < wagonAmount; i++)
        {
            trainAndWagons.Add(wagonPrefab);
        }

        // Instantiates the trainAndWagons-List with spacing in-between all the different cubes (train/wagons).
        for (int i = 0; i < trainAndWagons.Count; i++)
        {
            Instantiate(trainAndWagons[i], new Vector3(0, 0, i * 2), quaternion.identity);

            // Connects the rigidbodies of the next object (Wagon) to the previous object's Hinge Joint (Train/Wagon).
            if (i != 0)
            {
                trainAndWagons[i-1].GetComponent<HingeJoint>().connectedBody = trainAndWagons[i].GetComponent<Rigidbody>();
            }
        }
    }

    // Generates a value for acceleration every FixedUpdate and applies it to the train's rigidbody's velocity.
    private void FixedUpdate() {
        lastVelocity = GetComponent<Rigidbody>().velocity;
        trainAcceleration = (GetComponent<Rigidbody>().velocity - lastVelocity) / Time.fixedDeltaTime;

        trainAndWagons[0].GetComponent<Rigidbody>().velocity += trainAcceleration;
    }
}
