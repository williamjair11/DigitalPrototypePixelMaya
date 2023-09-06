using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class UIInventoryButton : MonoBehaviour
{
    public string objectId;
    public delegate void OnClickButton();
    public OnClickButton _onClickDelegate;
    public void OnClick()
    {
        GameManager.Instance.playerController.CanJump = false;
        GameManager.Instance.playerInventory.UseWeapon(objectId);
        _onClickDelegate?.Invoke();
    }

    public void AddListener(OnClickButton cb)
    {
        _onClickDelegate += cb;
    }
}
