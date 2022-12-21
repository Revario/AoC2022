using System.Net;
using System.Numerics;

var input = File.ReadAllText(Utils.Input.GetInputFilePath(11));

//input = "Monkey 0:\r\n  Starting items: 79, 98\r\n  Operation: new = old * 19\r\n  Test: divisible by 23\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 3\r\n\r\nMonkey 1:\r\n  Starting items: 54, 65, 75, 74\r\n  Operation: new = old + 6\r\n  Test: divisible by 19\r\n    If true: throw to monkey 2\r\n    If false: throw to monkey 0\r\n\r\nMonkey 2:\r\n  Starting items: 79, 60, 97\r\n  Operation: new = old * old\r\n  Test: divisible by 13\r\n    If true: throw to monkey 1\r\n    If false: throw to monkey 3\r\n\r\nMonkey 3:\r\n  Starting items: 74\r\n  Operation: new = old + 3\r\n  Test: divisible by 17\r\n    If true: throw to monkey 0\r\n    If false: throw to monkey 1";

//Console.WriteLine(RunMonkeyThrowing(false));
Console.WriteLine(RunMonkeyThrowing(true));

long RunMonkeyThrowing(bool part2)
{

    var monkeyInput = input.Split(Environment.NewLine + Environment.NewLine).Select(mI => Monkey.Parse(mI)).ToList();
    var red = monkeyInput.Select(m => m.Divisable).Aggregate((prev, next) => prev * next);

    var rounds = part2 ? 10000 : 20;

    for (int i = 0; i < rounds; i++)
    {
        if(i % 100 == 0)
        {
            Console.WriteLine(i);

        }
        for (var m = 0; m < monkeyInput.Count; m++)
        {
            var monkey = monkeyInput[m];

            while (monkey.Items.Any())
            {
                var o = monkey.Operation(monkey.Items[0]);
                if (!part2)
                {
                    o /= 3; 
                } else
                {
                    o = o % red;
                }
                monkey.Items[0] =  o;
                monkey.NumInspected++;
                monkeyInput[monkey.Action(monkey.Items[0])].Items.Add(monkey.Items[0]);
                monkey.Items.Remove(monkey.Items[0]);
            }
        }
    }

    return monkeyInput.OrderByDescending(m => m.NumInspected).Take(2).Select(m => m.NumInspected).Aggregate((prev, next) => prev * next);

}

class Monkey
{
    public List<BigInteger> Items { get; set; } = default!;
    public Func<BigInteger, BigInteger> Operation = default!;
    public Func<BigInteger, int> Action = default!;
    public int Divisable { get; set; }

    public long NumInspected { get; set; }


    public Monkey(List<BigInteger> items, Func<BigInteger, BigInteger> operation, Func<BigInteger, int> action, int divisable)
    {
        Items = items;
        Operation = operation;
        Action = action;
        Divisable = divisable;
    }

    public static Monkey Parse(string input)
    {

        var lines = input.Split("\n");

        var items = lines[1].Split(":")[1].Trim().Split(",").Select(s => BigInteger.Parse(s)).ToList();

        var operation = lines[2].Split("=")[1].Trim().Split(" ") switch
        {
            ["old", "*", "old"] => (BigInteger old) => old * old,
            ["old", "*", var numS] when BigInteger.TryParse(numS, out var num) => (Func<BigInteger, BigInteger>)((BigInteger old) => old * num),
            ["old", "+", var numS] when BigInteger.TryParse(numS, out var num) => (Func<BigInteger, BigInteger>)((BigInteger old) => old + num)
        };

        var modVal = GetLastWordAsLong(lines[3]);

        var trueMonkey = GetLastWordAsInt(lines[4]);
        var falseMonkey = GetLastWordAsInt(lines[5]);

        var action = (BigInteger value) => value % modVal == 0 ? trueMonkey : falseMonkey;

        return new Monkey(items, operation, action, (int)modVal);
    }

    private static ulong GetLastWordAsLong(string inp) => ulong.Parse(inp.Split(" ").Last());
    private static int GetLastWordAsInt(string inp) => int.Parse(inp.Split(" ").Last());
}
