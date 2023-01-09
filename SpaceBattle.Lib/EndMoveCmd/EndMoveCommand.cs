using Hwdtech;
namespace SpaceBattle.Lib;

public class EndMoveCommand : ICommand {
    private IMoveCommandEndable obj;

    public EndMoveCommand(IMoveCommandEndable other) {
        this.obj = other;
    }

    public void Execute() {
        obj.properties.ToList().ForEach(m => IoC.Resolve<ICommand>("SpaceBattle.EndMoveCmd.DeleteProperty", obj.uobject, m).Execute());
        IoC.Resolve<IInjectable>("SpaceBattle.EndMoveCmd.GetCommand", obj.uobject).Inject(IoC.Resolve<ICommand>("SpaceBattle.EndMoveCmd.Empty"));
    }

}
