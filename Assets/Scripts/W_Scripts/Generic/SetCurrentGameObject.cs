using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hubiese sido un mejor nombre SetFirstSelectedGameObject
public class SetCurrentGameObject : MonoBehaviour
{
    [SerializeField] GameObject _firstSelected;
    private GameObject _firstChild;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(_firstSelected != null)
        GameManager.Instance.uIController.SetCurrentSlectedGameObject(_firstSelected);
        else
        GameManager.Instance.uIController.SetCurrentSlectedGameObject(this.gameObject);
    }
}
