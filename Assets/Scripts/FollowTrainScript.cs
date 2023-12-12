using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrainScript : MonoBehaviour
{
    public GameObject train;

    // Update is called once per frame
    void LateUpdate () 
    {
        if (train == null && MainScript.mainInstance.trainsAndWagonsInstantiated == true)
        {
            train = MainScript.mainInstance.trainAndWagons[0];
        }
        else if (train != null)
        {
            transform.position = train.transform.position + new Vector3(-15.0f, 20.0f, -35.0f);
        }
    }
}
