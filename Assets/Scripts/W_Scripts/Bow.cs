using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bow : Weapon
{ 
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _arrowPoint;
    private GameObject _currentArrow;
    private float _arrowForce = 732;

    public override void PrepareAttack()
    {
        base.PrepareAttack();
        if(_arrowPoint.childCount < 1)
       _currentArrow = Instantiate(_arrowPrefab, _arrowPoint);
    }

    public override void PerformAttack()
    {
        base.PerformAttack();
        _damageController.CanMakeDamage = false;
        float fireStrength =  _damage * 100/ _maxDamage;
        fireStrength = fireStrength / 100;
        _currentArrow.GetComponent<ArrowScript>().Fire(_arrowForce * fireStrength);
    }

}
