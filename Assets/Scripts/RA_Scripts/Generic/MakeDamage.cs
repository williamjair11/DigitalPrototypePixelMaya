using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    [SerializeField] private float _damage = 5;
    [SerializeField] private List<string> _targetsId;

    public void OnTriggerStay(Collider other)
    {
        if(_targetsId.Contains(other.gameObject.tag))
        {
            other.gameObject.GetComponent<HealtController>()?.DecreaseHealt(_damage);
        }
    }
}
