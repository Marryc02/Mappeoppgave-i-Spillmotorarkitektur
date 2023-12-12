using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MainScript : MonoBehaviour
{

    public static MainScript mainInstance { get; private set; }

    public GameObject wagonPrefab;
    public GameObject smallTrainPrefab;
    public GameObject mediumTrainPrefab;
    public GameObject largeTrainPrefab;
    public GameObject customTrainPrefab;

    UIScript UIValues;

    [SerializeField] Canvas UIRef;

    string trainType;
    [HideInInspector] public float trainStartingVelocity;
    [HideInInspector] public float trainMass;
    [HideInInspector] public float customTrainMass;

    [HideInInspector] public int wagonAmount;
    [HideInInspector] public float wagonMass;


    List<GameObject> trainAndWagons;
    bool trainsAndWagonsInstantiated = false;
    bool hasValidTrainInfo = false;

    void Start()
    {
        mainInstance = this;

        UIValues = UIRef.GetComponent<UIScript>();
        if (UIValues == null)
        {
            Debug.Log("UI Reference in MainScript returned null!");
        }
        trainAndWagons = new List<GameObject>();
    }

    // Applies acceleration to the Rigidbody's velocity ever FixedUpdate if the train and its wagons have been instantiated.
    private void FixedUpdate() {
        if (trainsAndWagonsInstantiated == true)
        {
            //lastVelocity = trainAndWagons[0].GetComponent<Rigidbody>().velocity;
            //trainAcceleration = (trainAndWagons[0].GetComponent<Rigidbody>().velocity - lastVelocity) / Time.fixedDeltaTime;

            //trainAndWagons[0].GetComponent<Rigidbody>().velocity += trainAcceleration; 
        }
        
        // We need to only load these values once
        if (hasValidTrainInfo == false)
        {
            // Now to check if the packet recieved from the UI is valid
            if (UIValues.shippingPacket.validPacket == true)
            {
                // Assigning the values
                trainStartingVelocity = UIValues.shippingPacket.startVel;
                trainType = UIValues.shippingPacket.trainSelected;
                customTrainMass = UIValues.shippingPacket.cTrainMass;
                wagonMass = UIValues.shippingPacket.wagonWeight;
                wagonAmount = UIValues.shippingPacket.numWagons;
                Debug.Log("Values added");
                hasValidTrainInfo = true;
                SpawnTrainAndWagons();
            }
        }
    }

    void SpawnTrainAndWagons()
    {   
        // Adds an instance of a prefab of either type "Small", "Medium" or "Large" to the trainAndWagons-List and assigfns a given velocity.
        if (trainType == "Small Train") {
            trainAndWagons.Add(Instantiate(smallTrainPrefab, new Vector3(0, 0, 0), quaternion.identity));
            //trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainStartingVelocity;
            trainMass = 40000.0f;
        }
        else if (trainType == "Medium Train") {
            trainAndWagons.Add(Instantiate(mediumTrainPrefab, new Vector3(0, 0, 0), quaternion.identity));
            //trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainStartingVelocity;
            trainMass = 60000.0f;
        }
        else if (trainType == "Large Train") {
            trainAndWagons.Add(Instantiate(largeTrainPrefab, new Vector3(0, 0, 0), quaternion.identity));
            //trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainStartingVelocity;
            trainMass = 80000.0f;
        }
        else if (trainType == "Custom Train") {
            trainAndWagons.Add(Instantiate(customTrainPrefab, new Vector3(0, 0, 0), quaternion.identity));
            //trainAndWagons[0].GetComponent<Rigidbody>().velocity = trainStartingVelocity;
            trainMass = customTrainMass;
        }
        // Ensures an actual train type is selected.
        else {
            Debug.Log("No train type selected!");
        }

        // Adds an instance of the wagon prefab to the trainAndWagons list based on the specified amount of wagons the user wants.
        for (int i = 0; i < wagonAmount; i++)
        {
            trainAndWagons.Add(Instantiate(wagonPrefab, new Vector3((i + 1) * 2, 0, 0), quaternion.identity));
            trainAndWagons[i].GetComponent<FollowSpline>().spawnOffset = i * -5.0f;
        }
        
        // Connects the hinges of the train/wagons, starting at the wagon in the back to the train in the front.
        // for (int i = trainAndWagons.Count - 1; i > 0; i--)
        // {
        //     if (i != 0)
        //     {
        //         // Connects the Rigidbodies of the next object (Wagon) to the previous object's Hinge Joint (Train/Wagon).
        //         trainAndWagons[i].GetComponent<HingeJoint>().connectedBody = trainAndWagons[i - 1].GetComponent<Rigidbody>();
        //         Debug.Log("Connected Rigidody " + (i - 1) + " to HingeJoint " + i);
        //     }
        // }

        trainsAndWagonsInstantiated = true;
    }
}
