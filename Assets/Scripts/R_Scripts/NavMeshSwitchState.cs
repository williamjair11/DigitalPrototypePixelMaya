using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshSwitchState
{
    ICommand _onCommand;
    public NavMeshSwitchState(ICommand onCommand)
    {
        _onCommand = onCommand;
    }
    public void Chase()
    {
        _onCommand.Execute();
    }
    public void Patrol()
    {
        _onCommand.Execute();   
    }
}
