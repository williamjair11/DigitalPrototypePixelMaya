using UnityEngine;

public class GreenZone : MonoBehaviour
{
    [SerializeField] private bool _inZone = false;


    // Update is called once per fram

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        { 
            _inZone = true; 
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        { 
            _inZone = false; 
        }
    }
}
