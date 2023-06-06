using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using DG.Tweening;

public class ButtonActivated : MonoBehaviour
{
    [SerializeField] private Light buttonLight;
    [NonSerialized]public bool buttonIsActivated = false;
    void Start()
    {
        DOTween.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            buttonIsActivated = true;
            buttonLight.DOIntensity(50f, 2f);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            buttonIsActivated = false;
            buttonLight.DOIntensity(0f, 1f);
        }       
    }

    public bool estateButton() 
    {
        return buttonIsActivated;
    }
}
