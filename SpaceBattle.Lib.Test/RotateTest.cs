namespace SpaceBattle.Lib.Test;
using Moq;

public class RotateTest
{
    [Fact]
    public void PositiveTest()
    {
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty<Angle>(m => m.angle, new Angle (45, 1));
        rotatableMock.SetupGet<Angle>(m => m.angularVelocity).Returns(new Angle (90, 1));


        ICommand rotateCommand = new RotateCommand(rotatableMock.Object);
        rotateCommand.Execute();
        Assert.Equal(new Angle (135, 1),rotatableMock.Object.angle);



    }
    [Fact]
    public void setAngularException()
    {
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty(m => m.angle, new Angle(45, 1));
        rotatableMock.SetupGet<Angle>(m => m.angularVelocity).Returns(new Angle (90,1));
        rotatableMock.SetupSet<Angle>(m => m.angle = It.IsAny<Angle>()).Throws<Exception>();
        ICommand rotateCommand = new RotateCommand(rotatableMock.Object);

        Assert.Throws<Exception>(() => rotateCommand.Execute());
        

    }
    [Fact]
    public void getAngularException()
    {
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        
        rotatableMock.SetupGet<Angle>(m => m.angularVelocity).Returns(new Angle (45, 1));
        rotatableMock.SetupSet<Angle>(m => m.angle = new Angle (90, 1));
        rotatableMock.SetupGet<Angle>(m => m.angle).Throws<Exception>();
        ICommand rotateCommand = new RotateCommand(rotatableMock.Object);
        Assert.Throws<Exception>(() => rotateCommand.Execute());

    }
    [Fact]
    public void getAngularVelocityException()
    {
        Mock<IRotatable> rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty<Angle>(m => m.angle, new Angle (90, 1));
        rotatableMock.SetupGet<Angle>(m => m.angularVelocity).Throws<Exception>();
        ICommand rotateCommand = new RotateCommand(rotatableMock.Object);
        Assert.Throws<Exception>(() => rotateCommand.Execute());
    }

    
    
    


}