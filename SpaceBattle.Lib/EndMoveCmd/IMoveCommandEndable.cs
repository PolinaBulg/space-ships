namespace SpaceBattle.Lib;

public interface IMoveCommandEndable : ICommand {
    IUObject uobject { 
        get; 
    }
    IEnumerable<string> properties { 
        get; 
    }
}
