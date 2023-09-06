using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CreateExplosion : MonoBehaviour
{
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] bool _createExplosionOnCollide;
    [SerializeField] List<string> _collisionTags;
    [SerializeField] UnityEvent _onCreateExplosion;
    // Start is called before the first frame update
    
    public void InstantiateExplosion()
    {
       GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
       explosion.GetComponent<DestroyWithDelay>().Destroy(1f);
    }

     void OnCollisionEnter(Collision collision) {
        if (_collisionTags.Contains(collision.gameObject.tag) && _createExplosionOnCollide) 
        {
            InstantiateExplosion();
            _onCreateExplosion?.Invoke();
        }
    }
}
