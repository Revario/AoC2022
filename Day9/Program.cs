using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

var input = File.ReadLines(Utils.Input.GetInputFilePath(9));



var ins = input.Select(inp =>
inp.Split(" ") switch
    {
        ["R", var steps] => new Instruction(Direction.Right, int.Parse(steps)),
        ["L", var steps] => new Instruction(Direction.Left, int.Parse(steps)),
        ["U", var steps] => new Instruction(Direction.Up, int.Parse(steps)),
        ["D", var steps] => new Instruction(Direction.Down, int.Parse(steps)),
        _ => throw new ArgumentOutOfRangeException()
    }
).ToList();


//var part1 = new Rope(1);
var part2 = new Rope(9);

foreach(var i in ins)
{
    //part1.MoveHead(i);
    part2.MoveHead(i);
}

//Console.WriteLine(part1.NumberOfKnownTailPositions());
Console.WriteLine(part2.NumberOfKnownTailPositions());




class Rope
{
    HashSet<string> tailPositions = new();

    Head _head = new();
    Knot[] _knots;

    public Rope(int sizeOfTail)
    {
        _knots = new Knot[sizeOfTail];
        for (int i = 0; i < _knots.Length; i++)
        {
            _knots[i] = new();
        }
    }
    public int NumberOfKnownTailPositions()
    {
        return tailPositions.Count;
    }

    public void MoveHead(Instruction ins)
    {
        for (int i = 0; i < ins.numberOfSteps; i++)
        {
            _=ins.dir switch
            {
                Direction.Left => _head.x--,
                Direction.Right => _head.x++,
                Direction.Up => _head.y++,
                Direction.Down => _head.y--,
            };
            MoveTail(_head, _knots);
        }
    }

    private void MoveTail(Knot prev, Knot[] knots)
    {
        var k = knots.FirstOrDefault();

        if(k is null) { return; }

        if(Math.Abs(prev.x - k.x) == 2 )
        {
            k.y = prev.y;
            if(prev.x > k.x)
            {
                k.x++;
            } else
            {
                k.x--;
            }
        } else if(Math.Abs(prev.y - k.y) == 2 )
        {
            k.x = prev.x;
            if(prev.y > k.y)
            {
                k.y++;
            } else
            {
                k.y--;
            }
        }
        string stringPos = $"{k.x},{k.y}";
        if (knots.Length == 1 && !tailPositions.Contains(stringPos))
        {
            tailPositions.Add(stringPos);
        }
        MoveTail(k, knots[1..]);
    }

    class Head : Knot
    {
        //public int x;
        //public int y;
    }
    class Knot
    {
        public int x;
        public int y;
    }
}


enum Direction
{
    Left,
    Right,
    Up,
    Down
}
record Instruction(Direction dir, int numberOfSteps);