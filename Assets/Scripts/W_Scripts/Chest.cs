using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : InteractiveObject
{
    [SerializeField] UnityEvent _onOpenChest;
    private bool _chestIsOpen = false;
    [SerializeField] private List<InventoryObject>  _inventoryObjects;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player") 
        {
            if(!_chestIsOpen) 
            {
                GameManager.Instance.playerController.SetInteractiveObject(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") 
        {
                GameManager.Instance.playerController.SetNullInteractiveObject();
        }
    }

    //En este caso deje una dependencia con el uiController debido a que siempre existirá una relación directa
    //entre los cofres que no se puede evitar ni con unity events.
    public override void Interact()
    {
        base.Interact();
        _chestIsOpen = true;
        GameManager.Instance.uIController.SetInventoryObjects(_inventoryObjects);
        _onOpenChest?.Invoke();
        _playerCanInteract = false;
    }
}
