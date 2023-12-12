using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrainScript : MonoBehaviour
{
    public GameObject train;

    // Update is called once per frame
    void Update () 
    {
        if (train == null)
        {
            train = MainScript.mainInstance.trainAndWagons[0];
        }
        transform.position = train.transform.position + new Vector3(-15.0f, 20.0f, -35.0f);
    }
}
