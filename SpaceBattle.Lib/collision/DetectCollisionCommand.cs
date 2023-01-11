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
        var coordinatesFirst = IoC.Resolve<int[]>("Operations.GetProperty", firstObject, "Position");
        var coordinatesSecond = IoC.Resolve<int[]>("Operations.GetProperty", secondObject, "Position");

        var velocityFirst = IoC.Resolve<int[]>("Operations.GetProperty", firstObject, "Velocity");
        var velocitySecond = IoC.Resolve<int[]>("Operations.GetProperty", secondObject, "Velocity");

        var vectorDelta = IoC.Resolve<int[]>("Operations.CalculateDifferences", coordinatesFirst,velocityFirst, coordinatesSecond, velocitySecond);
        var solutionTree = IoC.Resolve<IDictionary<int, object>>("Operations.GetSolutionTree");

        var result = IoC.Resolve<bool>("Tools.SolutionTree.TreePass", solutionTree, vectorDelta);

        if (result)
        {
            throw new Exception();
        }

    }
}
