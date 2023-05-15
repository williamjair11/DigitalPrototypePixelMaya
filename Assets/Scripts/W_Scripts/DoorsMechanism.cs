using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsMechanism : MonoBehaviour
{
    private bool _doorIsActivated = false;
    [SerializeField] private ButtonActivated _estateButton1;
    [SerializeField] private ButtonActivated _estateButton2;
    [SerializeField] public bool _button_1_IsActivated = false;
    [SerializeField] public bool _button_2_IsActivated = false;


    private Animator _animatorDoor;

    void Start()
    {
        
        _animatorDoor = GetComponent<Animator>();
        _animatorDoor.SetBool("DoorActivated", false);
    }

    void Update()
    {
        _button_1_IsActivated = _estateButton1.buttonIsActivated;
        _button_2_IsActivated = _estateButton2.buttonIsActivated; 

        Debug.Log(_button_1_IsActivated);
        Debug.Log(_button_2_IsActivated);

        if (_button_1_IsActivated && _button_2_IsActivated) 
        {
            if(_doorIsActivated == false) 
            {
                openDoors();
            }
        }
        else 
        {
            if (_doorIsActivated) 
            {
                closedDoors();
            }
        }
    }

    void openDoors() 
    {
        _doorIsActivated = true;
        _animatorDoor.SetBool("DoorActivated",  _doorIsActivated);
    }

    void closedDoors() 
    {
        _doorIsActivated = false;
        _animatorDoor.SetBool("DoorActivated", _doorIsActivated);
    }
}
