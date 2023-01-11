namespace SpaceBattle.Lib;
using Hwdtech;

public class CalculateDifferencesStrategy : IStrategy
{
    
    public object Run(object[] parameters)
    {
        IUObject firstObject = (IUObject)parameters[0];
        IUObject secondObject = (IUObject)parameters[1];
        var vectorsDifFromDifferentProperties = new List<Vector>();
        var listFetchingProperties = IoC.Resolve<List<string>>("CalculateDifferences.ListProperties");
        listFetchingProperties.ForEach(property => vectorsDifFromDifferentProperties.Add((Vector)secondObject.GetProperty(property)-(Vector) firstObject.GetProperty(property)));
        return  vectorsDifFromDifferentProperties[0] + vectorsDifFromDifferentProperties[1];
    }
}
