using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public enum InteractionType
{
    Grab,
    Found,
    Push,
    TurnOnOff,
}
public class InteractiveObject : MonoBehaviour
{

    private UnityEvent _onInteract;
    [SerializeField] private bool _playerCanInteract = true;
    [SerializeField] private InteractionType _interactionType = InteractionType.Grab;
    public bool PlayerCanInteract{get => _playerCanInteract; set => _playerCanInteract = value;}
    public InteractionType InteractionType{get => _interactionType;}


    public virtual void Interact()
    {
    } 
    
}
