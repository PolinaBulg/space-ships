using Hwdtech;
namespace SpaceBattle.Lib;

public class MacroCmdStrategy : IStrategy {   
    public object Run(params object[] args) {
        string strategy = (string)args[0];
        IUObject obj = (IUObject)args[1];
        var operation = IoC.Resolve<IList<string>>("SpaceBattle.MacroCommandInitialization." + strategy);
        var list = new List<ICommand>();
        foreach (string step in operation) {
            list.Add(IoC.Resolve<ICommand>("SpaceBattle.MacroCommandInitialization." + step, obj));
        }
        return new MacroCommand(list);
    }
}
