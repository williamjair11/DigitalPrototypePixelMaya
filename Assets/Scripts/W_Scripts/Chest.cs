using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  

public class Chest : MonoBehaviour
{
    private InputController inputController;
    private bool _chestIsOpen = false;

    private void Start()
    {
        DOTween.Init();
        inputController = FindObjectOfType<InputController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            if (inputController.Interact() && _chestIsOpen == false) { OpenChest(); }
        }
    }

   public void OpenChest() 
   {
        transform.DORotate(new Vector3(-140f, 0, 0), 3);
        Debug.Log("abriendo");
        _chestIsOpen = true;
   }
}
