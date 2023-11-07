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
    float trainVelocity;
    float trainAcceleration;

    int wagonAmount = 3;
    float wagonMass;


    List<GameObject> trainAndWagons;


    void SpawnTrainAndWagons()
    {
        if (trainType == "Small") {
            trainAndWagons.Add(smallTrainPrefab);
            trainAndWagons[0].GetComponent<TrainClassScript>().velocity = trainVelocity;
            trainAndWagons[0].GetComponent<TrainClassScript>().acceleration = trainAcceleration;
        }
        else if (trainType == "Medium") {
            trainAndWagons.Add(mediumTrainPrefab);
            trainAndWagons[0].GetComponent<TrainClassScript>().velocity = trainVelocity;
            trainAndWagons[0].GetComponent<TrainClassScript>().acceleration = trainAcceleration;
        }
        else if (trainType == "Large") {
            trainAndWagons.Add(largeTrainPrefab);
            trainAndWagons[0].GetComponent<TrainClassScript>().velocity = trainVelocity;
            trainAndWagons[0].GetComponent<TrainClassScript>().acceleration = trainAcceleration;
        }
        else {
            Debug.Log("No train type selected!");
        }

        for (int i = 0; i < wagonAmount; i++)
        {
            trainAndWagons.Add(wagonPrefab);
            //trainAndWagons[i].GetComponent<Rigidbody>().mass = wagonMass;
        }

        for (int i = 0; i < trainAndWagons.Count; i++)
        {
            Instantiate(trainAndWagons[i], new Vector3(0, 0, i * 2), quaternion.identity);

            if (i != 0)
            {
                trainAndWagons[i-1].GetComponent<HingeJoint>().connectedBody = trainAndWagons[i].GetComponent<Rigidbody>();
            }
        }
    }
}
