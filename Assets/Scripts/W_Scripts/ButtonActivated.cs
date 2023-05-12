using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class ButtonActivated : MonoBehaviour
{
    private bool buttonIsActivated = false;

    [SerializeField] private Light buttonLight;
    void Start()
    {
        buttonLight = GetComponent<Light>();
        buttonLight.intensity = 0;
        buttonLight.range = 0;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        buttonIsActivated = true;
        buttonLight.intensity = 150f;
        buttonLight.range = 3.15f;
    }

    private void OnTriggerExit(Collider other)
    {
        buttonIsActivated = false;
        buttonLight.intensity = 0;
        buttonLight.range = 0;
    }

    public bool consultButton() 
    {
        return buttonIsActivated;
    }
}
