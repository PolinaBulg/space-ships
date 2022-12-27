namespace SpaceBattle.Lib;
using Hwdtech;

public class StartMoveCommand : ICommand
{
    private IMoveCommandStartable moveCommandStartable;
    public StartMoveCommand(IMoveCommandStartable moveCommandStartable)
    {
        this.moveCommandStartable = moveCommandStartable;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>("Operations.SetProperty", moveCommandStartable.Object, "Velocity", moveCommandStartable.Properties["Velocity"]);
        ICommand moveCommand = IoC.Resolve<ICommand>("Operations.MoveCommand", moveCommandStartable);
        IoC.Resolve<ICommand>("Collections.Queue.Push", moveCommandStartable.Queue, moveCommand).Execute();
        
    }
}

