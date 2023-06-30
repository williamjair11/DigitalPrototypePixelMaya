using System.Collections.Generic;


public class NavMeshView
{
    Stack<ICommand> _statesCommandList;
    Stack<ICommandAnimations> _animationsCommandList;
    public NavMeshView()
    {
        _statesCommandList = new Stack<ICommand>();
    }
    public void AddStateCommand(ICommand newCommnad)
    {
        newCommnad.Execute();
        _statesCommandList.Push(newCommnad);
    }
    public void AddAnimationCommand(ICommandAnimations newCommand)
    {
        newCommand.Execute();
        _animationsCommandList.Push(newCommand);
    }

}
