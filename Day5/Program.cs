using System.Runtime.CompilerServices;

var inputs = File.ReadAllText(new Utils.Input("5").GetInputFilePath());


var s = inputs.Split($"\n\n");

var stacksPart1 = BuildInitialStacks(s.First());
var stacksPart2 = BuildInitialStacks(s.First());

foreach(string instruction in s.Last().Split("\n"))
{
    if (string.IsNullOrWhiteSpace(instruction)) { continue; }
    var ins = instruction.Split(" ");
    var amount = int.Parse(ins[1]);
    var from = int.Parse(ins[3]) -1;
    var to = int.Parse(ins[5]) -1;

    Move(stacksPart1, amount, from, to);
    MovePart2(stacksPart2, amount, from, to);
}

var top1 = stacksPart1.Select(s => s.First.Value).ToArray();
var top2 = stacksPart2.Select(s => s.First.Value).ToArray();
Console.WriteLine(string.Join("", top1));
Console.WriteLine(string.Join("", top2));


void Move(LinkedList<char>[] stacks, int amount, int from, int to)
{
    //var tempStack = new LinkedList<char>();
    for (var n = 0; n < amount; n++)
    {
        var t = stacks[from].First!;
        stacks[from].RemoveFirst();
        stacks[to].AddFirst(t);
    }

    //while (tempStack.Any())
    //{
    //    var t = tempStack.First;
    //    tempStack.RemoveFirst();
    //    stacks[to].AddFirst(t);
    //}
}
void MovePart2(LinkedList<char>[] stacks, int amount, int from, int to)
{
    var tempStack = new LinkedList<char>();
    for (var n = 0; n < amount; n++)
    {
        var t = stacks[from].First!;
        stacks[from].RemoveFirst();
        tempStack.AddFirst(t);
    }

    while (tempStack.Any())
    {
        var t = tempStack.First;
        tempStack.RemoveFirst();
        stacks[to].AddFirst(t);
    }
}

LinkedList<char>[] BuildInitialStacks(string input)
{

    var numStacks = int.Parse(input.Split("\n").Last().Trim().Split(" ").Last());
    var stacks = new LinkedList<char>[numStacks];
    int[] positions = { 1, 5, 9, 13, 17, 21, 25, 29, 33 };
    //int[] positions = { 1, 5, 9 };

    foreach (string row in input.Split("\n")[..^1])
    {
        for(var i = 0; i<positions.Length; i++)
        {
            if (char.IsWhiteSpace(row[positions[i]]))
            {
                continue;
            }
            stacks[i] ??= new LinkedList<char>();
            stacks[i].AddLast(row[positions[i]]);
        }
    }

    return stacks;

}