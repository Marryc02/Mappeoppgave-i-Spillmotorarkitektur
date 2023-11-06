using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    string trainType;
    // Start is called before the first frame update
    void Start()
    {
        if (trainType == "Small") {

        }
        else if (trainType == "Medium") {

        }
        else if (trainType == "Large") {

        }
        else {
            Debug.Log("No train type selected!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
