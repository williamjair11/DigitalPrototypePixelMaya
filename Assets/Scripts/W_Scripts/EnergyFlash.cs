using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class EnergyFlash : SpecialAttack
{
    private Vector3 _standarSize;
    [SerializeField] private Vector3 _maxSize;
    [SerializeField] float _waveTime = 3f;
    [SerializeField] private SphereCollider _sphereCollider;

    void Awake()
    {
        if(_sphereCollider == null) _sphereCollider.GetComponent<SphereCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider.enabled = false;
        _maxSize = transform.localScale;
        _standarSize = Vector3.one * .1f;
        transform.localScale = _standarSize;
    }

    public void EnergyWave()
    {
        _sphereCollider.enabled = true;
        _sphereCollider.transform.DOScale(_maxSize, _waveTime).OnComplete(OnCompleteWave);
    }

    public void OnCompleteWave()
    {
        _sphereCollider.enabled = false;
        _sphereCollider.transform.DOScale(_standarSize, 0);
    }

    public override void PerformAttack()
    {
        base.PerformAttack();
        EnergyWave();
    }
}
