using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ReactToCollision : MonoBehaviour
{
    [SerializeField] List<string> _collisionTags = new List<string>();
    [SerializeField] float force = 59f;
    [SerializeField] UnityEvent _onCollision;
    
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision) {
        if (_collisionTags.Contains(collision.gameObject.tag)) 
        {
            if(collision.gameObject.GetComponent<DamageController>().CanMakeDamage)
            {
                Vector3 direction = transform.position - collision.transform.position;
                direction.y = 0;  // Mantén la misma altura, solo retrocede en el plano XZ

                  // Ajusta esta cantidad para cambiar cuánto retrocede el enemigo
                Vector3 position = transform.position + direction.normalized * force * Time.deltaTime;
                transform.DOMove(position, .3f);
                _onCollision?.Invoke();
            }
        }
    }
}
