using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffLight : MonoBehaviour
{
    public GameObject txtToDisplay;            

    private bool PlayerInZone;      

    public GameObject lightobj;

    private void Start()
    {

        PlayerInZone = false;                         
        txtToDisplay.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F)) //in zone and F key is pressed
        {
            lightobj.SetActive(!lightobj.activeSelf);
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //player in zone
        {
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }


    private void OnTriggerExit(Collider other)  //player exits zone
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }
}
