using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class ButtonActivated : MonoBehaviour
{
    [SerializeField] private Light buttonLight;
    [NonSerialized]public bool buttonIsActivated = false;
    void Start()
    {
        buttonLight = GetComponent<Light>();
        buttonLight.intensity = 0;
        buttonLight.range = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            buttonIsActivated = true;
            buttonLight.intensity = 150f;
            buttonLight.range = 3.15f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            buttonIsActivated = false;
            buttonLight.intensity = 0;
            buttonLight.range = 0;
        }
        
    }

    public bool estateButton() 
    {
        return buttonIsActivated;
    }
}
