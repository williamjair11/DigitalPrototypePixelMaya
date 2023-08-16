using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
[RequireComponent(typeof(DamageController))]
public class Weapon : InventoryObject
{

    [SerializeField] public int _maxDamage, _baseDamage, _damage;
    [SerializeField] Animator _animator;
    private float  _timeCounter, _maxTimeimeToPrepareAttak = 5;
    private bool _preparingAttack;
    protected DamageController _damageController;
    [SerializeField] UnityEvent _onPrepareAttack, _onPerformAttack;
    //private WeaponType weaponType;

    void Start()
    {
        if(_animator == null)
        _animator = GetComponent<Animator>();
        _damageController = GetComponent<DamageController>();
    }
    
    void Update()
    {
        if(_preparingAttack)
        {
            _timeCounter += Time.deltaTime;
        }
    }

    public virtual void PrepareAttack()
    {
        _preparingAttack = true;
        _animator.SetBool("PerformingAttack", false);
        _animator.SetBool("PreparingAttack", true);
        _onPrepareAttack?.Invoke();
    }

    public virtual void PerformAttack()
    {
        CalculateDamage();
        _damageController.DamageAmount = _damage;
        _damageController.CanMakeDamage = true;
        _preparingAttack = false;
        _animator.SetBool("PreparingAttack", false);
        _animator.SetBool("PerformingAttack", true);
        _onPerformAttack?.Invoke();
    }

    public virtual void SetIdleState()
    {
        _animator.SetBool("PreparingAttack", false);
        _animator.SetBool("PerformingAttack", false);
        SetWalkingState(false);
    }

    public virtual void SetWalkingState(bool value)
    {
        _animator.SetBool("WalkingState", value);
    }

    public void CalculateDamage()
    {
        _damage =  (int)Mathf.Round(_timeCounter * _maxDamage / _maxTimeimeToPrepareAttak);
        if(_damage > _maxDamage) _damage = _maxDamage;
        if(_damage < _baseDamage) _damage = _baseDamage;
        _timeCounter = 0;
    }

    public void SubscribeOnPrepareAttack(UnityAction subscriber)
    {
        _onPrepareAttack.AddListener(subscriber);
    }

    public void SubscribeOnPerformAttack(UnityAction subscriber)
    {
        _onPerformAttack.AddListener(subscriber);
    }

}
