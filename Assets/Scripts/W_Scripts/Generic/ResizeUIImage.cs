using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResizeUIImage : MonoBehaviour
{
    RectTransform _transform;
    float time = 0;
    [Range(0.01f, 1)] [SerializeField] float _resizeSpeed = .08f, _decreasementPercentage = .1f;
    [Range(0.01f, 1)]float _initialSize = .01f;
    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<RectTransform>();
       // _transform.sizeDelta = new Vector2(_initialSize, _initialSize);
    }
    

    public void MakeSmall()
    {
        StartCoroutine(ChangeSizeRoutine(-1));
    }

    public void MakeBig()
    {
        StartCoroutine(ChangeSizeRoutine());
    }

    void CheckSize(Vector2 newSize)
    {
        if(_transform.sizeDelta.x > 1) {newSize.x = 1; _transform.sizeDelta = newSize;}
        if(_transform.sizeDelta.y > 1) {newSize.y = 1; _transform.sizeDelta = newSize;}
        if(_transform.sizeDelta.y == 1 && _transform.sizeDelta.x == 1) StopCoroutine(ChangeSizeRoutine());
    }

    public IEnumerator ChangeSizeRoutine(float direction = 1)
    {
        Vector2 newSize = Vector2.zero;
        newSize.x = _transform.sizeDelta.x + (_decreasementPercentage * Mathf.Sign(direction));
        newSize.y = _transform.sizeDelta.y + (_decreasementPercentage * Mathf.Sign(direction));
        _transform.sizeDelta = newSize;
        Debug.Log("waitingToExpand");
        yield return new WaitForSeconds(_resizeSpeed);
        CheckSize(newSize);
        Debug.Log("Expanding");
    }
}
