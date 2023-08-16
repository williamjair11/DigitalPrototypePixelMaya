using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeSpriteColor : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _secondaryColor;
    [SerializeField] private Image _image;
    public void Start()
    {
        _image.color = _baseColor;
    }
    public void SetBaseColor()
    {
        _image.color = _baseColor;
    }

    public void SetSecondaryColor()
    {
        _image.color = _secondaryColor;
    }

    public void ToggleColor()
    {
        _image.color = _image.color == _baseColor? _secondaryColor : _baseColor;
    }
}
