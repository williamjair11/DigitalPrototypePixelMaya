using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator enemyaAnimator;
    int isWalkingHash;
    int isRunningHash;
    int isTurningHash;
    int isIdleHash;
    private EnemyMovement _enemyMovement;
    void Start()
    {
        enemyaAnimator = GetComponent<Animator>();
        _enemyMovement = FindObjectOfType<EnemyMovement>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isTurningHash = Animator.StringToHash("isTurning");
        isIdleHash = Animator.StringToHash("isIdle");
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool isWalking =  enemyaAnimator.GetBool(isWalkingHash);
        bool isRunning = enemyaAnimator.GetBool(isWalkingHash);
        bool isTurning = enemyaAnimator.GetBool(isTurningHash);
        bool isIdle = enemyaAnimator.GetBool(isIdleHash);
        /*
        if(!isWalking && forwardPressed)
        {
            enemyaAnimator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            enemyaAnimator.SetBool(isWalkingHash, false);
        }
        if (isWalking && runPressed)
        {
            enemyaAnimator.SetBool(isRunningHash, true);
        }
        if (!isWalking || !runPressed)
        {
            enemyaAnimator.SetBool(isRunningHash, false);
        }
        */
        if (_enemyMovement.GetEnemyState() == EnemyMovement.EnemyState.Walking)
        {
            enemyaAnimator.SetBool(isWalkingHash, true);
            enemyaAnimator.SetBool(isRunningHash, false);
            enemyaAnimator.SetBool(isTurningHash, false);
            enemyaAnimator.SetBool(isIdleHash, false);
        }
        else if ((_enemyMovement.GetEnemyState() == EnemyMovement.EnemyState.Running))
        {
            enemyaAnimator.SetBool(isRunningHash, true);
            enemyaAnimator.SetBool(isWalkingHash, false);
            enemyaAnimator.SetBool(isTurningHash, false);
            enemyaAnimator.SetBool(isIdleHash, false);
        }
        else if ((_enemyMovement.GetEnemyState() == EnemyMovement.EnemyState.Turning))
        {
            enemyaAnimator.SetBool(isTurningHash, true);
            enemyaAnimator.SetBool(isWalkingHash, false);
            enemyaAnimator.SetBool(isRunningHash, false);
            enemyaAnimator.SetBool(isIdleHash, false);
        }
        else if ((_enemyMovement.GetEnemyState() == EnemyMovement.EnemyState.Idle))
        {
            enemyaAnimator.SetBool(isIdleHash, true);
            enemyaAnimator.SetBool(isWalkingHash, false);
            enemyaAnimator.SetBool(isRunningHash, false);
            enemyaAnimator.SetBool(isTurningHash, false);
        }
    }
}
