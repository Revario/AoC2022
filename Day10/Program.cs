using System.Runtime.CompilerServices;

var input = File.ReadAllLines(Utils.Input.GetInputFilePath(10)).ToArray();

List<int> signalStrenghts = new();
List<string> litPositions = new();

int tickCounter = 0;
int register = 1;

Action noop = () => { };

foreach(var ins in input)
{
    (int cyclesForCurrentInstruction, Action instruction) = ins.Split(" ") switch
    {
        ["noop"] => (1, noop),
        ["addx", var regChange] when int.TryParse(regChange, out var changeBy) => (2, (Action)(() => { register += changeBy; }))
    };

    for (int i = 0; i < cyclesForCurrentInstruction; i++)
    {
        var y = tickCounter / 40;
        var x = tickCounter % 40;

        if(x >= register -1 && x <= register + 1)
        {
            litPositions.Add($"{x},{y}");
        }
        tick();
    }
    instruction.Invoke();
}


Console.WriteLine(signalStrenghts.Sum());

for (int y = 0; y < 6; y++)
{
    for (int x = 0; x < 40; x++)
    {
        Console.Write(litPositions.Contains($"{x},{y}") ? "X" : ".");
    }
    Console.WriteLine();
}
void tick()
{
    tickCounter++;
    if((tickCounter - 20) % 40 == 0)
    {
        signalStrenghts.Add(register * tickCounter);
    }
}
