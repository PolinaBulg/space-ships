namespace SpaceBattle.Lib;


public interface IMoveCommandStartable
{
    IUObject Object { get; }

    Vector Velocity { get; }

    IEnumerable<ICommand> Queue { get; }
}
