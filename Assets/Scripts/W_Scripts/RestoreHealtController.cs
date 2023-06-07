using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestoreHealtController : MonoBehaviour
{
    [SerializeField] public float _healtAmount;

    [SerializeField] private UnityEvent<float, string> OnRestoreHealt;

    private bool _canRestoreHealt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _canRestoreHealt)
        {
            OnRestoreHealt.Invoke(_healtAmount, other.tag);
        }
    }
    public void DesactivatedCanRestoreHealt()
    {
        _canRestoreHealt = false;
    }

    public void ActivatedCanRestoreHealt()
    {
        _canRestoreHealt = true;
    }
}
