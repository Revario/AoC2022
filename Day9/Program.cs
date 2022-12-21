
using System.Text;

var input = File.ReadLines(Utils.Input.GetInputFilePath(9));


//input = new string[] {

//    "R 4",
//"U 4",
//"L 3",
//"D 1",
//"R 4",
//"D 1",
//"L 5",
//"R 2"
//};

//input = new string[]
//{
//"R 5",
//"U 8",
//"L 8",
//"D 3",
//"R 17",
//"D 10",
//"L 25",
//"U 20"
//};


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


var part1 = new Rope(1);
var part2 = new Rope(9);

var field = new PlayingField();
foreach(var i in ins)
{
    part1.MoveHead(i);
    part2.MoveHead(i);
#if DEBUG
    PlayingField.PrintField(part2);
#endif
}

Console.WriteLine(part1.NumberOfKnownTailPositions());
Console.WriteLine(part2.NumberOfKnownTailPositions());



class PlayingField
{
    int size = 20;


    public static void PrintField(Rope r, string icon = "X")
    {
        Console.Clear();
        for (int y = 20; y > -20; y--)
        {
            var sb = new StringBuilder();
            for (int x = -20; x < 20; x++)
            {
                Console.Write(r.AnyKnotAtPosition(x,y) ? icon : " ");
            }
            Console.Write("\n");
        }
        Thread.Sleep(200);
    }
}

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
            //PlayingField.PrintField(this, "?");

        }
    }

    public bool AnyKnotAtPosition(int x, int y) => _head.x == x && _head.y == y || _knots.Any(k => k.x == x && k.y == y);
    private void MoveTail(Knot prev, Knot[] knots)
    {
        var k = knots.FirstOrDefault();

        if(k is null) { return; }

        if(Math.Abs(prev.x - k.x) == 2 && Math.Abs(prev.y - k.y) >= 2)
        {
            if(prev.x - k.x > 0)
            {
                k.x++;
            } else
            {
                k.x--;
            }
            if(prev.y - k.y > 0)
            {
                k.y++;
            } else
            {
                k.y--;
            }
        } else if(Math.Abs(prev.x - k.x) >= 2 )
        {
            k.y = prev.y;
            if(prev.x > k.x)
            {
                k.x++;
            } else
            {
                k.x--;
            }
        } else if(Math.Abs(prev.y - k.y) >= 2 )
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
        //PlayingField.PrintField(this, "#");
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

        public override string ToString()
        {
            return $"{x}:{y}";
        }
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