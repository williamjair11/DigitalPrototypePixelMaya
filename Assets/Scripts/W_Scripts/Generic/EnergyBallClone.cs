using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BlinkEffect), typeof(DestroyWithDelay))]
public class EnergyBallClone : MonoBehaviour
{
   [SerializeField] BlinkEffect _blinkEffect;
   [SerializeField] Rigidbody _rigidbody;
   [SerializeField] DestroyWithDelay _destroyWithDelay;

   public void ThrowBall(Vector3 trhowDirection, float _ballDestroyDelay)
   {
      float ballOffset = GameManager.Instance.playerController.GetComponent<CharacterController>().radius;
      transform.position = Camera.main.transform.position + Camera.main.transform.forward * (ballOffset + 2);
      _rigidbody.AddForce(trhowDirection * Time.deltaTime, ForceMode.Impulse);
      _blinkEffect.Blink(_ballDestroyDelay);
      _destroyWithDelay.Destroy(_ballDestroyDelay + .1f);
      
   }

}
