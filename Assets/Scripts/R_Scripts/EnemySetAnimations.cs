using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetAnimations : ICommandAnimations
{
    NavMeshAnimations _navMeshAnimation;
    string _newAnimation;
    public EnemySetAnimations(NavMeshAnimations navMeshAnimations, string newAnimation)
    {
        _navMeshAnimation = navMeshAnimations;
        _newAnimation = newAnimation;

    }

    public void Execute()
    {
        _navMeshAnimation.ChangeAnimationStateTo(_newAnimation);
    }
}
