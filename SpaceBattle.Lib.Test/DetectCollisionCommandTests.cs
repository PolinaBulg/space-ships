namespace SpaceBattle.Lib.Test;
using Moq;
using System;
using Xunit;
using Hwdtech.Ioc;
using Hwdtech;
using SpaceBattle.Lib;

public class CollisionDetecterTests
{
    void InitState()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockStrategyGetSolutionTree = new Mock<IStrategy>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.GetSolutionTree",(object[] parameters) => mockStrategyGetSolutionTree.Object.Run(parameters)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.CalculateDifference"); //передать страти=егию


    }
    [Fact]
    public void DetectCollision()
    {
        var mockUObject1 = new Mock<IUObject>();
        var mockUObject2 = new Mock<IUObject>();

        var solutionTree = IoC.Resolve<IDictionary<int, object>>("Operations.GetSolutionTree");
        var coordinatesDifference = IoC.Resolve<Vector>("Operations.CalculateDifference", mockUObject1.Object, mockUObject2.Object);
        var detectCollisionCommand =  new DetectCollisionCommand(mockUObject1.Object, mockUObject2.Object);

    }
}
