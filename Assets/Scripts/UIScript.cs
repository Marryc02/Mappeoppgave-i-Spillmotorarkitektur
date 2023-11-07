using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] TMP_InputField NumWagons;
    [SerializeField] TMP_InputField WagonWeight;
    [SerializeField] TMP_InputField StartVel;
    [SerializeField] TMP_InputField StartAcc;
    [SerializeField] TMP_InputField SlopeAngle;
    [SerializeField] TMP_InputField SlopeLength;
    [SerializeField] TMP_Dropdown TrainSelector;
    [SerializeField] Button StartButton;
    [SerializeField] TMP_Text ErrorText;

    public struct SettingsPacket
    {
        public SettingsPacket(int nWagons, float wWeight, float sVel, float sAcc, 
        float slAngle, float slLength, string tSelected, bool bValid)
        {
            numWagons = nWagons;
            wagonWeight = wWeight;
            startVel = sVel;
            startAcc = sAcc;
            slopeAngle = slAngle;
            slopeLength = slLength;
            trainSelected = tSelected;
            validPacket = bValid;
        }

        public SettingsPacket(bool bValid)
        {
            numWagons = 0;
            wagonWeight = 0.0f;
            startVel = 0.0f;
            startAcc = 0.0f;
            slopeAngle = 0.0f;
            slopeLength = 0.0f;
            trainSelected = "";
            validPacket = false;
        }

        public int numWagons;
        public float wagonWeight;
        public float startVel;
        public float startAcc;
        public float slopeAngle;
        public float slopeLength;
        public string trainSelected;
        public bool validPacket;
    }

    SettingsPacket validPacket;
    SettingsPacket invalidPacket = new SettingsPacket(false);

    public SettingsPacket shippingPacket;

    void Awake() 
    {
        ErrorText.enabled = false;
        shippingPacket = invalidPacket;
        StartButton.onClick.AddListener(StartSim);
    }

    void StartSim()
    {
        int iTest;
        float fTest;
        
        int nWagons = 0;
        float wWeight = 0.0f;
        float sVel = 0.0f;
        float sAcc = 0.0f;
        float slAngle = 0.0f;
        float slLength = 0.0f;

        if (int.TryParse(NumWagons.text, out iTest))
        {
            nWagons = int.Parse(NumWagons.text);
            if (nWagons < 0)
            {
                UpdateErrorText(0);
                return; 
            }
        } 
        else 
        {
            UpdateErrorText(6, "Number of wagons must be an integer");
            return;
        }

        if (float.TryParse(WagonWeight.text, out fTest) && fTest != 0)
        {
            wWeight = float.Parse(WagonWeight.text);
            if (wWeight < 40000.0f || wWeight > 80000.0f)
            { 
                UpdateErrorText(1);
                return;
            }
        }
        else 
        {
            UpdateErrorText(6, "Wagon weight must be a number");
            return;
        }

        if (float.TryParse(StartVel.text, out fTest))
        {
            sVel = float.Parse(StartVel.text);
            if (sVel < 0.0f) 
            {
               UpdateErrorText(2);
                return; 
            }
        } 
        else
        {
            UpdateErrorText(6, "Start velocity must be a number");
            return;    
        }

        if (float.TryParse(StartAcc.text, out fTest))
        {
            sAcc = float.Parse(StartAcc.text);
            if (sAcc < 0.0f)
            { 
                UpdateErrorText(3);
                return; 
            }
        } 
        else 
        {
            UpdateErrorText(6, "Start acceleration must be a number");
            return;
        }

        if (float.TryParse(SlopeAngle.text, out fTest))
        {
            slAngle = float.Parse(SlopeAngle.text);
            if (slAngle < 0.0f || slAngle > 45.0f)
            { 
                UpdateErrorText(4);
                return;
            }
        } 
        else 
        {
            UpdateErrorText(6, "Slope angle must be a number");
            return;
        }
        if (float.TryParse(SlopeLength.text, out fTest))
        {
            slLength = float.Parse(SlopeLength.text);
            if (slLength < 20.0f) 
            {
                UpdateErrorText(5);
                return; 
            }
        } else 
        {
            UpdateErrorText(6, "Slope length must be a number");
            return;
        }

        ErrorText.enabled = false;

        validPacket = new SettingsPacket
        (
            nWagons,
            wWeight,
            sVel,
            sAcc,
            slAngle,
            slLength,
            TrainSelector.options[TrainSelector.value].text,
            true
        );

        Debug.Log("Number of wagons: " + validPacket.numWagons);
        Debug.Log("Wagon weight: " + validPacket.wagonWeight + "kg");
        Debug.Log("Start velocity: " + validPacket.startVel + "km/h");
        Debug.Log("Start acceleration: " + validPacket.startAcc + "m/s^2");
        Debug.Log("Slope angle: " + validPacket.slopeAngle + "degrees");
        Debug.Log("Slope length: " + validPacket.slopeLength + "m");
        Debug.Log("Selected train: " + validPacket.trainSelected);

        shippingPacket = validPacket;
    }

    SettingsPacket GetPackage(SettingsPacket package)
    {
        return package;
    }

    void UpdateErrorText(int index, string message = "")
    {
        switch(index)
        {
            case 0:
                ErrorText.enabled = true;
                ErrorText.text = "Number of wagons must be greater than 0";
                break;
            case 1:
                ErrorText.enabled = true;
                ErrorText.text = "Weight must be between 40000 and 80000";
                break;
            case 2:
                ErrorText.enabled = true;
                ErrorText.text = "Start velocity must be greater than 0";
                break;
            case 3:
                ErrorText.enabled = true;
                ErrorText.text = "Start acceleration must be greater than 0";
                break;            
            case 4:
                ErrorText.enabled = true;
                ErrorText.text = "Slope must be between 0 and 45 degrees";
                break;
            case 5:
                ErrorText.enabled = true;
                ErrorText.text = "Slope must be longer that 20 meters";
                break;
            case 6:
                ErrorText.enabled = true;
                ErrorText.text = message;
                break;
            default:
                Debug.Log("Default switch case reached in UIScript.cs!");
                break;
        }
    }
}

