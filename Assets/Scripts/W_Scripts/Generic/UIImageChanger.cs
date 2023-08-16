using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIImageChanger : MonoBehaviour
{
    Image _image;
    Sprite _mainSprite;
    UnityEvent OnChangeImage;

    void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _mainSprite;
    }
    public void ChangeImage(Sprite _sprite)
    {
        _image.sprite = _mainSprite;
    }
}
