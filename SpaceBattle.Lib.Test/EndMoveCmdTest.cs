using Hwdtech;
using Hwdtech.Ioc;
using Moq;
namespace SpaceBattle.Lib.Test;

public class EndMoveCmdTest {
    public EndMoveCmdTest() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        Mock<SpaceBattle.Lib.ICommand> mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(m => m.Execute());
        Mock<IInjectable> mockInjectable = new Mock<IInjectable>();
        mockInjectable.Setup(m => m.Inject(It.IsAny<SpaceBattle.Lib.ICommand>()));
        
        Mock<IStrategy> mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(m => m.Run(It.IsAny<object[]>())).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.EndMoveCmd.DeleteProperty", (object[] args) => mockStrategyReturnsCommand.Object.Run(args)).Execute();
        
        Mock<IStrategy> mockStrategyReturnsInjectable = new Mock<IStrategy>();
        mockStrategyReturnsInjectable.Setup(m => m.Run(It.IsAny<object[]>())).Returns(mockInjectable.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.EndMoveCmd.GetCommand", (object[] args) => mockStrategyReturnsInjectable.Object.Run(args)).Execute();

        Mock<IStrategy> mockStrategyReturnsEmpty = new Mock<IStrategy>();
        mockStrategyReturnsEmpty.Setup(m => m.Run()).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.EndMoveCmd.Empty", (object[] args) => mockStrategyReturnsEmpty.Object.Run(args)).Execute();
    }

    [Fact]
    public void TestPositiveEndMoveCommandExecute() 
    {
        Mock<IMoveCommandEndable> mockEndable = new Mock<IMoveCommandEndable>();
        Mock<IUObject> mockUobj = new Mock<IUObject>();
        mockEndable.SetupGet(m => m.uobject).Returns(mockUobj.Object).Verifiable();
        mockEndable.SetupGet(m => m.properties).Returns(new List<string>() {"Velocity"}).Verifiable();
        ICommand endCmd = new EndMoveCommand(mockEndable.Object);
        endCmd.Execute();
    }

    [Fact]
    public void TestNegativeGetUobject()
    {
        Mock<IMoveCommandEndable> mockEndable = new Mock<IMoveCommandEndable>();
        mockEndable.SetupGet(m => m.uobject).Throws<Exception>().Verifiable();
        mockEndable.SetupGet(m => m.properties).Returns(new List<string>() {"Velocity"}).Verifiable();
        ICommand endCmd = new EndMoveCommand(mockEndable.Object);
        Assert.Throws<Exception>(() => endCmd.Execute());
    }

    [Fact]
    public void TestNegativeGetProperties()
    {
        Mock<IMoveCommandEndable> mockEndable = new Mock<IMoveCommandEndable>();
        Mock<IUObject> mockUobj = new Mock<IUObject>();
        mockEndable.SetupGet(m => m.uobject).Returns(mockUobj.Object).Verifiable();
        mockEndable.SetupGet(m => m.properties).Throws<Exception>().Verifiable();
        ICommand endCmd = new EndMoveCommand(mockEndable.Object);
        Assert.Throws<Exception>(() => endCmd.Execute());
    }

}
