namespace SpaceBattle.Lib.Test;
using Moq;
using System;
using Xunit;
using Hwdtech.Ioc;
using Hwdtech;
using SpaceBattle.Lib;

public class DetectCollisionCommandTests
{
    bool isCollision = false;
    void InitState()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var mockGetListProperiesStrategy = new Mock<IStrategy>();
        var mockSolutionTreePassStrategy = new Mock<IStrategy>();
        
        mockGetListProperiesStrategy.Setup(x => x.Run(It.IsAny<object[]>())).Returns(new List<string>{"Position", "Velocity"});
        mockSolutionTreePassStrategy.Setup(x => x.Run(It.IsAny<object[]>())).Returns(isCollision);
        var calculateDifferencesStrategy = new CalculateDifferencesStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CalculateDifferences.ListProperties", (object[] parameters) => mockGetListProperiesStrategy.Object.Run(parameters)).Execute(); 
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.CalculateDifferences", (object[] parameters) => calculateDifferencesStrategy.Run(parameters)).Execute(); 
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Tools.SolutionTree.TreePass", (object[] parameters) => mockSolutionTreePassStrategy.Object.Run(parameters)).Execute(); 


    }
    [Fact]
    public void CollisionNotHappends()
    {
        InitState();
        var mockUObject1 = new Mock<IUObject>();
        var mockUObject2 = new Mock<IUObject>();
        mockUObject1.Setup(x =>  x.GetProperty("Position")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject1.Setup(x => x.GetProperty("Velocity")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject2.Setup(x => x.GetProperty("Position")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject2.Setup(x => x.GetProperty("Velocity")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        
        var detectCollisionCommand = new DetectCollisionCommand(mockUObject1.Object,mockUObject2.Object );
        
        detectCollisionCommand.Execute();

        


    }
    [Fact]
    public void CollisionHappends()
    {
        isCollision = true;
        InitState();
        var mockUObject1 = new Mock<IUObject>();
        var mockUObject2 = new Mock<IUObject>();
        mockUObject1.Setup(x =>  x.GetProperty("Position")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject1.Setup(x => x.GetProperty("Velocity")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject2.Setup(x => x.GetProperty("Position")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject2.Setup(x => x.GetProperty("Velocity")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        
        var detectCollisionCommand = new DetectCollisionCommand(mockUObject1.Object,mockUObject2.Object );
        
        Assert.Throws<Exception>(() => detectCollisionCommand.Execute());

        


    }
}
