namespace SpaceBattle.Lib.Test;
using Moq;
using System;
using Xunit;
using Hwdtech.Ioc;
using Hwdtech;
using SpaceBattle.Lib;


public class StartMoveCommandTests
{
    void InitState()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<Lib.ICommand>();
        var mockStrategyGetCommand = new Mock<IStrategy>();

        mockCommand.Setup(x => x.Execute());
        mockStrategyGetCommand.Setup(x => x.Run(It.IsAny<object[]>())).Returns(mockCommand.Object);


        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.SetProperty", (object[] parameters) => mockStrategyGetCommand.Object.Run(parameters)).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Operations.MoveCommand", (object[] parameters) => mockStrategyGetCommand.Object.Run(parameters)).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Collections.Queue.Push", (object[] parameters) => mockStrategyGetCommand.Object.Run(parameters)).Execute();
    }

    [Fact]

    public void StartMoveCommandSuccessful()
    {
        InitState();
        var mockTarget = new Mock<IUObject>();
        var mockMoveCommandStartable = new Mock<IMoveCommandStartable>();

        mockMoveCommandStartable.SetupGet<IUObject>(x => x.Object).Returns(mockTarget.Object).Verifiable();
        mockMoveCommandStartable.SetupGet<IDictionary<string, object>>(x => x.Properties).Returns(new Dictionary<string, object> { { "Velocity", new Vector(1, 2) } }).Verifiable();
        mockMoveCommandStartable.SetupGet<IEnumerable<Lib.ICommand>>(x => x.Queue).Returns(new Queue<Lib.ICommand>()).Verifiable();

        var startMoveCommand = new StartMoveCommand(mockMoveCommandStartable.Object);
        startMoveCommand.Execute();

        mockMoveCommandStartable.Verify();
    }

    [Fact]

    public void TryRunStartMoveComandWithoutInitialVelocity()
    {
        InitState();
        var mockTarget = new Mock<IUObject>();
        var mockMoveCommandStartable = new Mock<IMoveCommandStartable>();

        mockMoveCommandStartable.SetupGet<IUObject>(x => x.Object).Returns(mockTarget.Object).Verifiable();
        mockMoveCommandStartable.SetupGet<IDictionary<string, object>>(x => x.Properties).Throws(new Exception()).Verifiable();
        mockMoveCommandStartable.SetupGet<IEnumerable<Lib.ICommand>>(x => x.Queue).Returns(new Queue<Lib.ICommand>()).Verifiable();

        var startMoveCommand = new StartMoveCommand(mockMoveCommandStartable.Object);

        Assert.Throws<Exception>(() => startMoveCommand.Execute());
    }

    [Fact]
    public void TryRunStartMoveComandWithoutTarget()
    {
        InitState();
        var mockTarget = new Mock<IUObject>();
        var mockMoveCommandStartable = new Mock<IMoveCommandStartable>();

        mockMoveCommandStartable.SetupGet<IUObject>(x => x.Object).Throws(new Exception()).Verifiable();
        mockMoveCommandStartable.SetupGet<IDictionary<string, object>>(x => x.Properties).Returns(new Dictionary<string, object> { { "Velocity", new Vector(1, 2) } }).Verifiable();
        mockMoveCommandStartable.SetupGet<IEnumerable<Lib.ICommand>>(x => x.Queue).Returns(new Queue<Lib.ICommand>()).Verifiable();

        var startMoveCommand = new StartMoveCommand(mockMoveCommandStartable.Object);

        Assert.Throws<Exception>(() => startMoveCommand.Execute());
    }
}
