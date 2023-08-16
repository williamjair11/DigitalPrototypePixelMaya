using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    private Quaternion _initialRotation;

    void Start()
    {
        _initialRotation = transform.rotation;
    }
}
