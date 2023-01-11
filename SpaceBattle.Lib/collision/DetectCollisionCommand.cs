namespace SpaceBattle.Lib;
using Hwdtech;
public class DetectCollisionCommand : ICommand
{
    private IUObject firstObject, secondObject;
    public DetectCollisionCommand(IUObject firstObject, IUObject secondObject)
    {
        this.firstObject = firstObject;
        this.secondObject = secondObject;
    }
    public void Execute()
    {
        var vectorDelta = IoC.Resolve<Vector>("Operations.CalculateDifferences", firstObject,secondObject);
        var result = IoC.Resolve<bool>("Tools.SolutionTree.TreePass", vectorDelta);
        if (result)
        {
            throw new Exception("Collision has been detected!");
        }
    }
}
