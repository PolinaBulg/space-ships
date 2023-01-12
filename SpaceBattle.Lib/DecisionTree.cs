using Hwdtech;
namespace SpaceBattle.Lib;

public class DecisionTree : ICommand {
    private string path;
    public DecisionTree(string other) {
        this.path = other;
    }

    public void Execute() {
        var tree = IoC.Resolve<Dictionary<int, object>>("SpaceBattle.GetDecisionTree");
        try {
            var vec = File.ReadAllLines(path).ToList().Select(line => line.Split().Select(int.Parse).ToList()).ToList();
            vec.ForEach(list => { 
                var temp = tree; 
                list.ForEach(item => { 
                    temp.TryAdd(item, new Dictionary<int, object>()); 
                    temp = (Dictionary<int, object>) temp[item];
                });
            });
        }
        catch (FileNotFoundException err) {
            throw new FileNotFoundException(err.ToString());
        }
        catch (Exception err) {
            throw new Exception(err.ToString());
        }
    }
}
