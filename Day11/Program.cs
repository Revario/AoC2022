var input = File.ReadAllText(Utils.Input.GetInputFilePath(11));



var monkeyInput = input.Split("\n\n");

Console.WriteLine(monkeyInput[0]);







class Monkey
{
    public List<int> Items { get; set; } = default!;
    private Func<int, int> Operation = default!;
    private Action<int> Action = default!;


    public Monkey(List<int> items, Func<int, int> operation, Action<int> action)
    {
        Items = items;
        Operation = operation;
        Action = action;
    }

    public static Monkey Parse(string input)
    {

        var lines = input.Split("\n");

        var items = lines[1].Split(":")[1].Trim().Split(",").Select(s => int.Parse(s)).ToList();

        var opLine = lines[2].Split("=")[1].Trim().Split(" ") switch
        {
            ["old", "*", "old"] => (int old) => old * old,
            ["old", "*", var numS] when int.TryParse(numS, out var num) => (Func<int,int>)((int old) => old * num),
            ["old", "+", var numS] when int.TryParse(numS, out var num) => (Func<int, int>)((int old) => old + num)

        };
    }
}