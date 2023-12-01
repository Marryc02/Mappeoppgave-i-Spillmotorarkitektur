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

    UIScript UIValues;

    [SerializeField] Canvas UIRef;

    string trainType;
    Vector3 trainVelocity;
    //Vector3 lastVelocity;
    Vector3 trainAcceleration;

    int wagonAmount;
    float wagonMass;


    List<GameObject> trainAndWagons;
    bool trainsAndWagonsInstantiated = false;
    bool hasValidTrainInfo = false;

    void Start()
    {
        UIValues = UIRef.GetComponent<UIScript>();
        if (UIValues == null)
        {
            Debug.Log("UI Reference is MainScript returned null!");
        }
        trainAndWagons = new List<GameObject>();
    }

    void SpawnTrainAndWagons()
    {   
        // Adds a prefab of either type "Small", "Medium" or "Large" to the trainAndWagons-List and assigfns a given velocity.
        if (trainType == "Small Train") {
            trainAndWagons.Add(smallTrainPrefab);
            trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainVelocity;
        }
        else if (trainType == "Medium Train") {
            trainAndWagons.Add(mediumTrainPrefab);
            trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainVelocity;
        }
        else if (trainType == "Large Train") {
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
        // for (int i = 0; i < trainAndWagons.Count; i++)
        // {
        //     Instantiate(trainAndWagons[i], new Vector3(0, 0, i * 2), quaternion.identity);

        //     // Connects the Rigidbodies of the next object (Wagon) to the previous object's Hinge Joint (Train/Wagon).
        //     if (i != 0)
        //     {
        //         trainAndWagons[i-1].GetComponent<HingeJoint>().connectedBody = trainAndWagons[i].GetComponent<Rigidbody>();
        //         Debug.Log("Run through");
        //     }
        // }

        // int x = 0;

        // // Essentially only keep going through the while loop if x + 1 can be a valid index
        // while (x + 1 < trainAndWagons.Count - 1)
        // {
        //     Instantiate(trainAndWagons[x], new Vector3(0, 0, x * 2), quaternion.identity);
        //     // connectedBody might instead be connectedAnchor, it at least won't make
        //     // a connection using connectedBody right now.
        //     trainAndWagons[x].GetComponent<HingeJoint>().connectedBody = trainAndWagons[x+1].GetComponent<Rigidbody>();
        //     x++;
        // }

        trainsAndWagonsInstantiated = true;
    }

    // Applies acceleration to the Rigidbody's velocity ever FixedUpdate if the train and its wagons have been instantiated.
    private void FixedUpdate() {
        if (trainsAndWagonsInstantiated == true)
        {
            //lastVelocity = trainAndWagons[0].GetComponent<Rigidbody>().velocity;
            //trainAcceleration = (trainAndWagons[0].GetComponent<Rigidbody>().velocity - lastVelocity) / Time.fixedDeltaTime;

            trainAndWagons[0].GetComponent<Rigidbody>().velocity += trainAcceleration; 
        }
        
        // We need to only load these values once
        if (hasValidTrainInfo == false)
        {
            // Now to check if the packet recieved from the UI is valid
            if (UIValues.shippingPacket.validPacket == true)
            {
                // Assigning the values
                trainVelocity = new Vector3(UIValues.shippingPacket.startVel, 0, 0);
                trainAcceleration = new Vector3(UIValues.shippingPacket.startAcc, 0, 0);
                trainType = UIValues.shippingPacket.trainSelected;
                wagonMass = UIValues.shippingPacket.wagonWeight;
                wagonAmount = UIValues.shippingPacket.numWagons;
                Debug.Log("Values added");
                hasValidTrainInfo = true;
                SpawnTrainAndWagons();
            }
        }
    }
}
