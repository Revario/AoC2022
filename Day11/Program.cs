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

}