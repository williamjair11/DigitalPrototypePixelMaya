using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class AnimationController : MonoBehaviour
{
    private string _currentState;
    [HideInInspector] public Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        AnimatorController _controller = _animator.runtimeAnimatorController as AnimatorController;
    }
    public void ChangeAnimationStateTo(string _newState)
    {
        if (_currentState == _newState) return;
        _animator.Play(_newState);
        _currentState = _newState;
    }
}
