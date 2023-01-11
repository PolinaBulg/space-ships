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

        var mockStrategyCalculateDifference = new Mock<IStrategy>();
        mockStrategyCalculateDifference.Setup(x => x.Run(It.IsAny<object[]>())).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>()));
        var mockStrategyCheckCollision = new Mock<IStrategy>();
        mockStrategyCheckCollision.Setup(x => x.Run(It.IsAny<object[]>())).Returns(true);

    
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.CalculateDifference", (object[] parameters) => mockStrategyCalculateDifference.Object.Run(parameters)).Execute(); 
        


    }
    [Fact]
    public void DetectCollision()
    {
        var mockUObject1 = new Mock<IUObject>();
        var mockUObject2 = new Mock<IUObject>();
        
        mockUObject1.SetupGet(x => x.GetProperty("Position")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject1.SetupGet(x => x.GetProperty("Velocity")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject2.SetupGet(x => x.GetProperty("Position")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        mockUObject2.SetupGet(x => x.GetProperty("Velocity")).Returns(new Vector(It.IsAny<int>(),It.IsAny<int>())).Verifiable();
        
        var detectCollisionCommand = new DetectCollisionCommand(mockUObject1.Object,mockUObject2.Object );
        
        detectCollisionCommand.Execute();

        


    }
}
