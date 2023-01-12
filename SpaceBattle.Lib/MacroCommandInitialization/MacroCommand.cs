namespace SpaceBattle.Lib;

public class MacroCommand : ICommand {   
    private List<ICommand> list;

    public MacroCommand(List<ICommand> other) {
        this.list = other;
    }

    public void Execute() {
        foreach (ICommand cmd in list) {
            cmd.Execute();
        }
    }
}
