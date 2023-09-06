using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class ResizeUIImage : MonoBehaviour
{
    [SerializeField] RectTransform _transform;
    float time = 0;
    [Range(0.01f, 1)] [SerializeField] float _resizeSpeed = .08f;
    [Range(0.01f, 1)]float _normalSize;
    // Start is called before the first frame update
    void Awake()
    {
        _normalSize = _transform.localScale.x;
        _transform = GetComponent<RectTransform>();
    }
    
    public void SetNormalSize()
    {
        _transform.DOScale(_normalSize, _resizeSpeed);
    }

    public void MakeSmall()
    {
        _transform.DOScale(_normalSize * .5f, _resizeSpeed);
    }

    public void MakeBig()
    {
        _transform.DOScale(_normalSize * .6f + _normalSize, _resizeSpeed);
    }

    
}
