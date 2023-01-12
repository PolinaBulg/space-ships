using Hwdtech;
using Hwdtech.Ioc;
using Moq;
namespace SpaceBattle.Lib.Test;

public class MacroCommandInitializationTest {
    public MacroCommandInitializationTest() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        Mock<ICommand> mockCommand = new Mock<ICommand>();
        mockCommand.Setup(m => m.Execute());

        Mock<IStrategy> mockStrategyReturnsList = new Mock<IStrategy>();
        mockStrategyReturnsList.Setup(m => m.Run()).Returns(new List<string> {"Steps"});

        Mock<IStrategy> mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(m => m.Run(It.IsAny<object[]>())).Returns(mockCommand.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.MacroCommandInitialization.Operations", (object[] args) => mockStrategyReturnsList.Object.Run(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.MacroCommandInitialization.Steps", (object[] args) => mockStrategyReturnsCommand.Object.Run(args)).Execute();
    }

    [Fact]
    public void TestPositiveMacroCommandInitializationExecute() {
        var macroCmdStrategy = new MacroCmdStrategy();
        var obj = new Mock<IUObject>();
        var macroCmd = (MacroCommand)macroCmdStrategy.Run("Operations", obj.Object);
        macroCmd.Execute();
    }
}
