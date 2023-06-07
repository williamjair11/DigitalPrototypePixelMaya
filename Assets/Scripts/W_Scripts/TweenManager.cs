using UnityEngine;
using DG.Tweening;
public class TweenManager : MonoBehaviour
{
    #region Doors Variables
    [Header("Doors Tween")]
    [SerializeField] GameObject _doorLeft;
    [SerializeField] GameObject _doorRight;
    [SerializeField] private float _speedDoors;
    [SerializeField] private float _openDoorLeftPosition;
    [SerializeField] private float _openDoorRighPosition;
    [SerializeField] private float _closeDoorsLeftPosition;
    [SerializeField] private float _closeDoorsRightPosition;
    #endregion
    void Start()
    {
        DOTween.Init();
    }

    #region Doors Tween
    public void OpenDoor() 
    {
        _doorLeft.transform.DOMoveX(_openDoorLeftPosition, _speedDoors);
        _doorRight.transform.DOMoveX(_openDoorRighPosition, _speedDoors);
    }

    public void CloseDoor() 
    {
        _doorLeft.transform.DOMoveX(_closeDoorsLeftPosition, _speedDoors);
        _doorRight.transform.DOMoveX(_closeDoorsRightPosition, _speedDoors);
    }
    #endregion
}
