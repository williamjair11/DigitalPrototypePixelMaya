using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    [SerializeField] UnityEvent _onDestroy;

    public void Destroy(float destroyDelayTime = 0)
    {
        if(gameObject && destroyDelayTime == 0) Destroy(gameObject);
        else
        if(gameObject)
        {
            Debug.Log(gameObject.name);
            StartCoroutine(DestroyRutine(destroyDelayTime));
        }
        
    }

    public IEnumerator DestroyRutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(!gameObject.activeSelf) gameObject.SetActive(true);
        _onDestroy?.Invoke();
        Destroy(gameObject);
    }
}