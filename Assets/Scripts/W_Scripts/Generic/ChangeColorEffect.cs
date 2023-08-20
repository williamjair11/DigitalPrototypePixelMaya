using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeColorEffect : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Color _secondaryColor;
    Color _baseColor;
    [SerializeField] float _effectTime;
    // Start is called before the first frame update
    void Start()
    {
        _material.color = _baseColor;
    }

    // Update is called once per frame
    public void ChangeColor()
    {
        Sequence colorSecuence = DOTween.Sequence();
        colorSecuence.Append(_material.DOColor(_secondaryColor, _effectTime));
        colorSecuence.Append(_material.DOColor(_baseColor, _effectTime));
    }

}
