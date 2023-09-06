using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class EnergyBalls : SpecialAttack
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _throwStrength, _ballDestroyDelay;

    void Awake()
    {
        _ballPrefab.GetComponent<Material>();
    }

    public override void PerformAttack()
    {
        Vector3 trhowDirection = Camera.main.transform.forward * _throwStrength;
        base.PerformAttack();
        GameObject ball = Instantiate(_ballPrefab);
        ball.GetComponent<EnergyBallClone>().ThrowBall(trhowDirection, _ballDestroyDelay);
    }


}
