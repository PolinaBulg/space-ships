namespace SpaceBattle.Lib.Test;

public class AngleTest
{
    [Fact]
    public void Positive()
    {
        Angle a = new Angle(45, 1);
        Angle b = new Angle(90, 2);
        Assert.Equal(a,b);

    }

    [Fact]
    public void PositivSum()
    {
        Angle a = new Angle(45, 90);
        Angle b = new Angle(45, 90);
        Assert.Equal(new Angle(1, 1),a + b);

    }   
    [Fact]
    public void NegativeSum()
    {
        Angle a = new Angle(45, 1);
        Angle b = new Angle(90, 2);
        Assert.False(a + b == new Angle(90, 2));

    }



    [Fact]
    public void DivizionByZeroException()
    {
        Assert.Throws<Exception>(()=> new Angle(99,0)); 

    }

     [Fact]
    public void CheckHashCode()
    {
        Angle a = new Angle(45, 1);
        Angle b = new Angle(90, 2);
        Assert.True(a.GetHashCode() == b.GetHashCode());
    }
    
       [Fact]
    public void CheckHashCodeNeg()
    {
        Angle a = new Angle(42, 1);
        Angle b = new Angle(90, 2);
        Assert.True(a.GetHashCode() != b.GetHashCode());
    }
    
    [Fact]
    public void SumResult()
    {
        Angle a = new Angle(45, 1);
        Angle b = new Angle(90, 2);
        Angle c  = a + b;
        Assert.Equal(c,a + b);

    }

    [Fact]
    public void CheckNOD()
    {
        int nod = Angle.NOD(4, 5);
        Assert.Equal(1, nod);
    }
    
    [Fact]
    public void NegEquel()
    {
        Angle a = new Angle(45, 1);
        Angle b = new Angle(90, 2);
        Assert.False(a != b);
    }

    [Fact]
    public void PosEquel()
    {
        Angle a = new Angle(45, 1);
        Assert.False(a.Equals("String"));
    }
    
   






    



}