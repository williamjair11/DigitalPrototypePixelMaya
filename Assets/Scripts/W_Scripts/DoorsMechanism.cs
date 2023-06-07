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
    private TweenManager _tweenManager;


    void Start()
    { 
        numButtons = _buttons.Count;
        _tweenManager = FindObjectOfType<TweenManager>();
        _tweenManager.CloseDoor();
    }

    void Update()
    {
        scanButtons();

        if (buttonsIsActivated)
        {
            if (_doorIsActivated == false)
            {
                _tweenManager.OpenDoor();
                _doorIsActivated = true;
            }
        }
        else
        {
            if (_doorIsActivated)
            {
                _tweenManager.CloseDoor();
                _doorIsActivated = false;
            }
        }
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
