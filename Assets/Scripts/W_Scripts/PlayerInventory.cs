using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Sword,
    Bow,
    SpecialAttack
}
    
public class PlayerInventory : MonoBehaviour
{
    Dictionary<string, InventoryObject>  _inventory = new Dictionary<string, InventoryObject>();


    public void AddObject(InventoryObject inventoryObject)
    {
        if(!_inventory.ContainsKey(inventoryObject.id))
        _inventory.Add(inventoryObject.id, inventoryObject);
    }

    public List<InventoryObject> GetObjectsByType(ObjectType type)
    {
        List<InventoryObject> result = new List<InventoryObject>();
        foreach (InventoryObject inventoryObject in _inventory.Values)
        {
            if(inventoryObject.type == type)
            {
                result.Add(inventoryObject);
            }
        }
        return result;
    }

    public InventoryObject GetObjectById(string id)
    {
        InventoryObject result = _inventory.ContainsKey(id)? _inventory[id] : null;
        return result;
    }

    public List<InventoryObject> GetAll()
    {
        List<InventoryObject> result = new List<InventoryObject>();
        foreach (InventoryObject inventoryObject in _inventory.Values)
        {
            result.Add(inventoryObject);
        }
        return result;
    }

    public void UseWeapon(string id)
    {
        if(_inventory.ContainsKey(id))
        {
            if(_inventory[id].type == ObjectType.SpecialAttack)
            GameManager.Instance.playerController.SetSpecialAttack((SpecialAttack)_inventory[id]);
            else
            GameManager.Instance.playerController.SetWeapon((Weapon)_inventory[id]);
        }
    }
}
