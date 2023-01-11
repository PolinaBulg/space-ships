namespace SpaceBattle.Lib.Test;
using Moq;
using System;
using Xunit;
using Hwdtech.Ioc;
using Hwdtech;
using SpaceBattle.Lib;

public class DetectCollisionCommandTests
{
    void InitState()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockStrategyGetSolutionTree = new Mock<IStrategy>();
        mockStrategyGetSolutionTree.Setup(x => x.Run(It.IsAny<object[]>())).Returns(new Dictionary<int,object>());
        var mockStrategyCalculateDifference = new Mock<IStrategy>();
        mockStrategyCalculateDifference.Setup(x => x.Run(It.IsAny<object[]>())).Returns(new Vector(1,4,6,6));
        var mockStrategyCheckCollision = new Mock<IStrategy>();
        mockStrategyCheckCollision.Setup(x => x.Run(It.IsAny<object[]>())).Returns(true);

        

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.GetSolutionTree",(object[] parameters) => mockStrategyGetSolutionTree.Object.Run(parameters)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.CalculateDifference", (object[] parameters) => mockStrategyCalculateDifference.Object.Run(parameters)).Execute(); 
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Tools.SolutionTree.TreePass", (object[] parameters) => mockStrategyCheckCollision.Object.Run(parameters)).Execute(); 


    }
    [Fact]
    public void DetectCollision()
    {
        var mockUObject1 = new Mock<IUObject>();
        var mockUObject2 = new Mock<IUObject>();
        

        var detectCollisionCommand = new DetectCollisionCommand(mockUObject1.Object,mockUObject2.Object );
        detectCollisionCommand.Execute();


    }
}
