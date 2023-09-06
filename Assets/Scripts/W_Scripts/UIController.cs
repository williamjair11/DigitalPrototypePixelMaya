using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] private GameObject _objectMessageContainer, _touchControls, _pauseMenu;
    [SerializeField] private GameObject 
    _hud, 
    _interactionMessage, 
    _shortMenu, 
    shortMenuDynamicContainer,
    _shortMenuItemPrefab,
    _shortMenuWorldCanvas,
    _shortMenuDynamicContainerWorldCanvas;
    [SerializeField] private Image _objectMessageImage, _interactionIcon;
    [SerializeField] TextMeshProUGUI _objectMessageText;
    [SerializeField] private List<InventoryObject> _inventoryObjects = new List<InventoryObject>();
    [SerializeField] Sprite _psIcon, _xboxIcon, _touchIcon;
    private GameObject _lastShorMenuButtonSelected;
    [SerializeField] Color _selectedButtonColor;

    public bool SetActiveTouchControls
    {
        get => _touchControls.activeSelf; 
        set 
        {
            if(GameManager.Instance.CurrentControlType == ControlType.Touch)
                _touchControls.SetActive(value);
            else
                _touchControls.SetActive(false);
        } 
    }

    public bool SetActiveHud
    {
        get => _hud.activeSelf; set => _hud.SetActive(value);
    }

    public bool SetActivePauseMenu
    {
        get => _pauseMenu.activeSelf; 
        set 
        {
            _pauseMenu.SetActive(value);
            if(_pauseMenu.activeSelf) 
                GameManager.Instance.SetGameState(GameState.Pause);
            else
                GameManager.Instance.SetGameState(GameState.InGame);
        }
    }

    public void SetCurrentSlectedGameObject(GameObject newSelectedObject)
    {
        _eventSystem.SetSelectedGameObject(newSelectedObject);
    }

    public void SetCurrentSelectedShortMenuButtonColor()
    {
        if(GameManager.Instance.CurrentGameState == GameState.Pause) return;
        if(GameManager.Instance.CurrentControlType != ControlType.Touch)
        {
            if(_lastShorMenuButtonSelected == null) _lastShorMenuButtonSelected = _eventSystem.currentSelectedGameObject;
            if(_shortMenuWorldCanvas.activeSelf || _shortMenuDynamicContainerWorldCanvas.activeSelf)
            {
                _lastShorMenuButtonSelected.GetComponent<Image>().color = Color.white;   
                _lastShorMenuButtonSelected = _eventSystem.currentSelectedGameObject;
                _eventSystem.currentSelectedGameObject.GetComponent<Image>().color = _selectedButtonColor;
            }
        }
    }
    public void ToglePauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        if(_pauseMenu.activeSelf) 
            GameManager.Instance.SetGameState(GameState.Pause);
        else
            GameManager.Instance.SetGameState(GameState.InGame);
    }

    public bool SetInteractionMessage
    {
      get =>  _interactionMessage.activeSelf;  
      set 
        {
            if(value && 
            GameManager.Instance.playerController.CurrentInteractiveObject &&
            GameManager.Instance.playerController.CurrentInteractiveObject.PlayerCanInteract)
                _interactionMessage.SetActive(true);
            else
                _interactionMessage.SetActive(false);
        }
    }

    public bool TogleShortMenu 
    { 
        get =>_shortMenu.activeSelf; 
        set {
            if(GameManager.Instance.CurrentGameState == GameState.Pause) return;
                if(GameManager.Instance.CurrentControlType == ControlType.Touch)
                {
                    shortMenuDynamicContainer.SetActive(false);
                    
                    _shortMenu.SetActive(!_shortMenu.activeSelf);
                }
                else
                {
                    _shortMenuDynamicContainerWorldCanvas.SetActive(false);
                    DestroyChildren(_shortMenuDynamicContainerWorldCanvas.transform);
                    _shortMenuWorldCanvas.SetActive(!_shortMenuWorldCanvas.activeSelf);
                }
                
            }
    }
    
    public bool SetActiveShortDynamicMenu 
    { 
        get =>shortMenuDynamicContainer.activeSelf; 
        set {
            if(GameManager.Instance.CurrentGameState == GameState.Pause) return;
            if(GameManager.Instance.CurrentControlType == ControlType.Touch)
            {
                if(shortMenuDynamicContainer.transform.childCount > 0)
                {
                    _shortMenu.SetActive(false);
                    shortMenuDynamicContainer.SetActive(value);
                }
            }
            else
            {
                if(_shortMenuDynamicContainerWorldCanvas.transform.childCount > 0)
                {
                    _shortMenuWorldCanvas.SetActive(false);
                    _shortMenuDynamicContainerWorldCanvas.SetActive(value);
                }
            }
                
            }
    }

    public void HideShortMenu()
    {
        if(GameManager.Instance.CurrentControlType == ControlType.Touch)
        _shortMenu.SetActive(false);
        else
        _shortMenuWorldCanvas.SetActive(false);
    }

    public bool ShowingShortMenu()
    {
        if(_shortMenuWorldCanvas.activeSelf || _shortMenuDynamicContainerWorldCanvas.activeSelf)
        return true;
        else 
        return false;
    }
    void Update()
    {
        SetCurrentSelectedShortMenuButtonColor();
        if(_inventoryObjects.Count > 0 && !_objectMessageContainer.activeSelf) 
        {
            ShowFoundObjectMessage();
        }
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
        GameObject DynamicContainer = 
        GameManager.Instance.CurrentControlType == ControlType.Touch? 
        shortMenuDynamicContainer : _shortMenuDynamicContainerWorldCanvas;
        ObjectType objType = GetTypeByString(type);
        if(DynamicContainer.transform.childCount > 0)
        {
            DestroyChildren(DynamicContainer.transform);
        }
        List<InventoryObject> objects = GameManager.Instance.playerInventory.GetObjectsByType(objType);
        foreach (InventoryObject invObject in objects)
        {
            GameObject itemPrefab = Instantiate(_shortMenuItemPrefab, DynamicContainer.transform);
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
        GameManager.Instance.SetGameState(GameState.Pause);
    }

    public void HideFoundObjectMessage()
    {
        if(_inventoryObjects.Count >= 1)
        _inventoryObjects.Remove(_inventoryObjects[_inventoryObjects.Count-1]);
        _objectMessageContainer.SetActive(false);
        SetActiveTouchControls = true;
        SetActiveHud = true;
        SetInteractionMessage = false;
        GameManager.Instance.SetGameState(GameState.InGame);
    }

    public void DestroyChildren(Transform parent)
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
