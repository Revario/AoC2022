var input = File.ReadAllText(Utils.Input.GetInputFilePath(11));

//input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";

var monkeyInput = input.Split("\n\n").Select(mI => Monkey.Parse(mI)).ToList();

for (int i = 0; i < 20; i++)
{
    for(var m = 0; m < monkeyInput.Count; m++)
    {
        var monkey = monkeyInput[m];

        while (monkey.Items.Any())
        {
            monkey.Items[0] = monkey.Operation(monkey.Items[0]) / 3;
            monkey.NumInspected++;
            monkeyInput[monkey.Action(monkey.Items[0])].Items.Add(monkey.Items[0]);
            monkey.Items.Remove(monkey.Items[0]);
        }
    }
}



int monkeyBussiness = monkeyInput.OrderByDescending(m => m.NumInspected).Take(2).Select(m => m.NumInspected).Aggregate((prev, next) => prev * next);

Console.WriteLine(monkeyBussiness);



class Monkey
{
    public List<int> Items { get; set; } = default!;
    public Func<int, int> Operation = default!;
    public Func<int,int> Action = default!;

    public int NumInspected { get; set; }


    public Monkey(List<int> items, Func<int, int> operation, Func<int,int> action)
    {
        Items = items;
        Operation = operation;
        Action = action;
    }

    public static Monkey Parse(string input)
    {

        var lines = input.Split("\n");

        var items = lines[1].Split(":")[1].Trim().Split(",").Select(s => int.Parse(s)).ToList();

        var operation = lines[2].Split("=")[1].Trim().Split(" ") switch
        {
            ["old", "*", "old"] => (int old) => old * old,
            ["old", "*", var numS] when int.TryParse(numS, out var num) => (Func<int, int>)((int old) => old * num),
            ["old", "+", var numS] when int.TryParse(numS, out var num) => (Func<int, int>)((int old) => old + num)
        };

        var modVal = GetLastWordAsInt(lines[3]);

        var trueMonkey = GetLastWordAsInt(lines[4]);
        var falseMonkey = GetLastWordAsInt(lines[5]);

        var action = (int value) => value % modVal == 0 ? trueMonkey : falseMonkey;

        return new Monkey(items, operation, action);
    }

    private static int GetLastWordAsInt(string inp) => int.Parse(inp.Split(" ").Last());
}