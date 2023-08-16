using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextDebuguer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMesh;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessageText(string text)
    {
        _textMesh.text = text;
    }
}
