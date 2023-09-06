using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private UnityEvent _onStopBlink;
    [SerializeField] private Color _secondaryColor = Color.white;
    [SerializeField] private float _duration = 5f, _interval;
    [SerializeField] private Material material;
    [SerializeField] private bool _blinkOnStart;
    private Color _baseColor;
    private float _timeCounter = 0;
    

    void Awake()
    {
        if(material == null) material = GetComponent<Material>();
        _baseColor = material.color;
    }

    void Update()
    {
        _timeCounter += Time.deltaTime;
    }

    public void Blink(float blinkDuration = 0)
    {
        if(blinkDuration == 0) blinkDuration = _duration;
        Color nextColor = material.color == _secondaryColor? _baseColor : _secondaryColor;
        material.DOColor(nextColor, _interval)
        .OnComplete( () => 
        { 
            if(_timeCounter < blinkDuration) Blink(blinkDuration);
            else _onStopBlink?.Invoke();
        });
        
        
    }

    void OnDestroy()
    {
        DOTween.Kill(material);
    }
    
}
