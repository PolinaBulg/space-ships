using Hwdtech;
using Hwdtech.Ioc;
using Moq;
namespace SpaceBattle.Lib.Test;

public class DecisionTreeTest{
    public DecisionTreeTest() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        Mock<IStrategy> mockStrategyReturnsTree = new Mock<IStrategy>();
        mockStrategyReturnsTree.Setup(m => m.Run(It.IsAny<object[]>())).Returns(new Dictionary<int, object>());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceBattle.GetDecisionTree", (object[] args) => mockStrategyReturnsTree.Object.Run(args)).Execute();
    }

    [Fact]
    public void TestPositiveDecisionTreeBuilding() {
        string path = "../../../vec.txt";
        DecisionTree tree = new DecisionTree(path);
        tree.Execute();
    }

    [Fact]
    public void TestNegativeDecisionTreeBuildingThrowsException() {
        string path = "";
        DecisionTree tree = new DecisionTree(path);
        Assert.Throws<Exception>(() => tree.Execute());
    }

    [Fact]
    public void TestNegativeDecisionTreeBuildingThrowsFileNotFoundException() {
        string path = "../file.txt";
        DecisionTree tree = new DecisionTree(path);
        Assert.Throws<FileNotFoundException>(() => tree.Execute());
    }
}
