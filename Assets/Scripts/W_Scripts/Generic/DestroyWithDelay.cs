using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    public void Destroy(float destroyDelayTime = 0)
    {
        Destroy(gameObject, destroyDelayTime);
    }
}
