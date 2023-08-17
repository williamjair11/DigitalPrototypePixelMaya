using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _objectMessageContainer, _touchControls, _pauseMenu;
    [SerializeField] private GameObject 
    _hud, 
    _interactionMessage, 
    _shortMenu, 
    shortMenuDynamicContainer,
    _shortMenuItemPrefab;
    [SerializeField] private Image _objectMessageImage, _interactionIcon;
    [SerializeField] TextMeshProUGUI _objectMessageText;
    [SerializeField] private List<InventoryObject> _inventoryObjects = new List<InventoryObject>();
    [SerializeField] Sprite _psIcon, _xboxIcon, _touchIcon;

    public bool SetActiveTouchControls
    {
        get => _touchControls.activeSelf; set => _touchControls.SetActive(value);
    }
    public bool SetActiveHud
    {
        get => _hud.activeSelf; set => _hud.SetActive(value);
    }

    public bool SetActivePauseMenu
    {
        get => _pauseMenu.activeSelf; set => _pauseMenu.SetActive(value);
    }

    public void ToglePauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }

    public bool SetInteractionMessage
    {
      get =>  _interactionMessage.activeSelf;  set => _interactionMessage.SetActive(value);
    }

    public bool TogleShortMenu 
    { 
        get =>_shortMenu.activeSelf; 
        set {
                shortMenuDynamicContainer.SetActive(false);
                _shortMenu.SetActive(!_shortMenu.activeSelf);
            }
    }
    
    public bool SetActiveShortDynamicMenu 
    { 
        get =>shortMenuDynamicContainer.activeSelf; 
        set {
                if(shortMenuDynamicContainer.transform.childCount > 0)
                {
                    _shortMenu.SetActive(false);
                    shortMenuDynamicContainer.SetActive(value);
                }
            }
    }

    public void HideShortMenu()
    {
        _shortMenu.SetActive(false);
    }

    void Update()
    {
        if(_inventoryObjects.Count > 0 && !_objectMessageContainer.activeSelf) 
        {
            ShowFoundObjectMessage();
        }

        //if(_pauseMenu.activeSelf) GameManager.Instance.setGameState(pause);
    }

    public void SetInteractionMessageIcon()
    {
        if(GameManager.Instance.CurrentGamepadType == GamepadType.dualShok)
        _interactionIcon.sprite = _psIcon;
        else
        if(GameManager.Instance.CurrentGamepadType == GamepadType.XInput)
        _interactionIcon.sprite = _xboxIcon;
        else
        _interactionIcon.sprite = _touchIcon;
    }

    public void SetShortMenuItems(string type)
    {
        Debug.Log(type);
        ObjectType objType = GetTypeByString(type);
        if(shortMenuDynamicContainer.transform.childCount > 0)
        {
            DistroyChildren(shortMenuDynamicContainer.transform);
        }
        List<InventoryObject> objects = GameManager.Instance.playerInventory.GetObjectsByType(objType);
        foreach (InventoryObject invObject in objects)
        {
            GameObject itemPrefab = Instantiate(_shortMenuItemPrefab, shortMenuDynamicContainer.transform);
            itemPrefab.GetComponent<Image>().sprite = invObject.sprite;
            UIInventoryButton inventoryButton = itemPrefab.GetComponent<UIInventoryButton>();
            inventoryButton.objectId = invObject.id;
            inventoryButton.AddListener(HideShortDynamicMenu);
        }
    }

    public void HideShortDynamicMenu()
    {
        SetActiveShortDynamicMenu = false;
    }

    public void SetInventoryObjects(List<InventoryObject> objects)
    {
        _inventoryObjects = objects;
    }

    void ShowFoundObjectMessage()
    {
        InventoryObject inventoryObject =  _inventoryObjects[_inventoryObjects.Count-1];
        GameManager.Instance.playerInventory.AddObject(inventoryObject);
        _objectMessageImage.sprite = inventoryObject.sprite;
        _objectMessageText.text = inventoryObject.descrition;
        SetActiveTouchControls = false;
        SetActiveHud = false;
        _objectMessageContainer.SetActive(true);
    }

    public void HideFoundObjectMessage()
    {
        if(_inventoryObjects.Count >= 1)
        _inventoryObjects.Remove(_inventoryObjects[_inventoryObjects.Count-1]);
        _objectMessageContainer.SetActive(false);
        SetActiveTouchControls = true;
        SetActiveHud = true;
        SetInteractionMessage = false;
    }

    public void DistroyChildren(Transform parent)
    {
        foreach (Transform item in parent)
            {
                item.GetComponent<DestroyWithDelay>().Destroy();
            }
    }

    public ObjectType GetTypeByString(string type)
    {
        if(type == ObjectType.Bow.ToString())
        return ObjectType.Bow;
        else
        if(type == ObjectType.Sword.ToString())
        return ObjectType.Sword;
        else
        return ObjectType.SpecialAttack;
    }

}
