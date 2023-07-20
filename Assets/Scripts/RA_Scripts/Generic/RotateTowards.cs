using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class RotateTowards : MonoBehaviour
{
      // The speed of the rotation.
  public float rotationSpeed = 1.0f;

  // The object to rotate.
  public GameObject objectToRotate;

  // Update is called once per frame.
  void Update() {
    // Get the object's velocity.
    Rigidbody rigidbody = objectToRotate.GetComponent<Rigidbody>();
    Vector3 velocity = rigidbody.velocity;

    // Rotate the object in the direction of its velocity.
    objectToRotate.transform.Rotate(velocity.normalized * rotationSpeed);
  }

}
