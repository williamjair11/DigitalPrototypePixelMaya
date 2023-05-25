using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsMechanism : MonoBehaviour
{
    private bool _doorIsActivated = false;
    [SerializeField] private List<ButtonActivated> _buttons;
    private int numButtons;
    private int currentButtons=0;
    private bool buttonsIsActivated;

    private Animator _animatorDoor;

    void Start()
    {
        
        _animatorDoor = GetComponent<Animator>();
        _animatorDoor.SetBool("DoorActivated", false);
        numButtons = _buttons.Count;
    }

    void Update()
    {
        scanButtons();

        if (buttonsIsActivated)
        {
            if (_doorIsActivated == false)
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

    void scanButtons() 
    {
        for(int i = 0; i < numButtons; i++) 
        {
            if (_buttons[i].buttonIsActivated) 
            {
                currentButtons++;
            }
        }

        if(currentButtons == numButtons) 
        {
            buttonsIsActivated = true;
        }
        else 
        {
            buttonsIsActivated = false;
        }

        currentButtons = 0;
    }
}
