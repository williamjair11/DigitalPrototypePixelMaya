using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class UpdateVariableOverTime : MonoBehaviour
{

    public bool _updatingVariable;
    [SerializeField] public float _delayToUpdate = 1;
    public float _valueFactor = .05f;
    [SerializeField] UnityEvent<float> _onUpdateVariable;

    public void StartUpdateVariable(bool increaseValue, float variable)
    {
        _updatingVariable = true;
        StartCoroutine(UpdateVariable(increaseValue, variable));
    }

    public void StopUpdateVariable()
    {
        _updatingVariable = false;
        StopAllCoroutines();
    }

    public IEnumerator UpdateVariable(bool increaseValue, float variable)
        {
            while(_updatingVariable)
            {
                yield return new WaitForSeconds(_delayToUpdate);
                if(increaseValue)
                variable += _valueFactor;
                else
                variable -= _valueFactor;
                _onUpdateVariable?.Invoke(variable);
            }
    }
}
