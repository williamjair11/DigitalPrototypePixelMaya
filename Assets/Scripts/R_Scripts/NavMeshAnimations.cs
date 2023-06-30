using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class NavMeshAnimations : MonoBehaviour
{
    private string _currentState;
    [HideInInspector] public Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void ChangeAnimationStateTo(string _newState)
    {
        if (_currentState == _newState) return;
        _animator.CrossFade(_newState, 1f);
        //_animator.Play(_newState);
        _currentState = _newState;
    }
}
