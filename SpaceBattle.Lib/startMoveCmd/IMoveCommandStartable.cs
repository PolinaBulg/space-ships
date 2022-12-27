namespace SpaceBattle.Lib;


public interface IMoveCommandStartable
{
    IUObject Object { get; }

    IDictionary<string, object> Properties { get; }

    IEnumerable<ICommand> Queue { get; }
}
