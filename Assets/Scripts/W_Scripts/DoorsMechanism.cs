using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsMechanism : MonoBehaviour
{
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;
    private bool _doorIsActivated = false;
    private bool _buttonIsActivated = false;
    public ButtonActivated _estateButton;
    void Start()
    {
        _estateButton = GetComponent<ButtonActivated>();
    }

    // Update is called once per frame
    void Update()
    {
        _buttonIsActivated = _estateButton.consultButton();

        if (_buttonIsActivated) 
        {
            _doorIsActivated = true;
        }

        if (_doorIsActivated) 
        {
            if(doorRight.transform.localPosition.x < 6.5) 
            {
                doorRight.Translate(Vector3.right * Time.deltaTime);
            }
            if (doorLeft.transform.localPosition.x < -12.5)
            {
                doorLeft.Translate(Vector3.left * Time.deltaTime);
            }
        }
        else if (_doorIsActivated == false) 
        {

        }
    }
}
