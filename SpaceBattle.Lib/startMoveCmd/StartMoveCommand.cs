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
        IoC.Resolve<ICommand>("Operations.SetProperty", moveCommandStartable.Object, "Velocity", moveCommandStartable.Velocity);
        ICommand moveCommand = IoC.Resolve<ICommand>("Commands.MoveCommand", moveCommandStartable);
        IoC.Resolve<ICommand>("Collections.Queue.Push", moveCommandStartable.Queue, moveCommand).Execute();
        
    }
}

