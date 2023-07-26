using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsMechanism : MonoBehaviour
{ 
    enum MechanismType {Buttons, Torchs}
    [SerializeField] MechanismType _type;

    #region Doors Variables
    [Header("Doors Tween")]
    [SerializeField] GameObject _doorLeft;
    [SerializeField] GameObject _doorRight;
    [SerializeField] private float _speedDoors;
    [SerializeField] private float _openDoorLeftPosition;
    [SerializeField] private float _openDoorRighPosition;
    [SerializeField] private float _closeDoorsLeftPosition;
    [SerializeField] private float _closeDoorsRightPosition;
    private bool _doorIsActivated = false;
    #endregion

    [Header("Buttons Variables")]   
    [SerializeField] private List<ButtonActivated> _buttons;
    private int numButtons;
    private int currentButtons=0;
    private bool buttonsIsActivated;

    [Header("Torchs Variables")]
    [SerializeField] private List<TurnOnOffLight> _torchs;
    private int numTorchs;
    private int currentTorchs=0;
    private bool torchsIsActivated;

    void Start()
    {
        DOTween.Init();
        numButtons = _buttons.Count;
        numTorchs = _torchs.Count;
        CloseDoor();
    }

    void Update()
    {
        switch (_type) 
        {
            case MechanismType.Buttons:
                scanButtons();

                if (buttonsIsActivated)
                {
                    if (_doorIsActivated == false)
                    {
                        OpenDoor();
                        _doorIsActivated = true;
                    }
                }
                else
                {
                    if (_doorIsActivated)
                    {
                        CloseDoor();
                        _doorIsActivated = false;
                    }
                }
            break;

                case MechanismType.Torchs:
                ScanTorch();

                if (torchsIsActivated)
                {
                    if (_doorIsActivated == false)
                    {
                        OpenDoor();
                        _doorIsActivated = true;
                    }
                }
                else
                {
                    if (_doorIsActivated)
                    {
                        CloseDoor();
                        _doorIsActivated = false;
                    }
                }
            break;
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

    void ScanTorch() 
    {
        for (int i = 0; i < numTorchs; i++)
        {
            if (_torchs[i]._torchTurnedOn)
            {
                currentTorchs++;
            }
        }

        if (currentTorchs == numTorchs)
        {
            torchsIsActivated = true;
        }
        else
        {
            torchsIsActivated = false;
        }

        currentTorchs = 0;
    }

    void OpenDoor() 
    {
        _doorLeft.transform.DOMoveX(_openDoorLeftPosition, _speedDoors);
        _doorRight.transform.DOMoveX(_openDoorRighPosition, _speedDoors);
    }

    void CloseDoor() 
    {
        _doorLeft.transform.DOMoveX(_closeDoorsLeftPosition, _speedDoors);
        _doorRight.transform.DOMoveX(_closeDoorsRightPosition, _speedDoors);
    }
}
