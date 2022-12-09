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


var r = new Rope();

foreach(var i in ins)
{
    r.MoveHead(i);
}

Console.WriteLine(r.NumberOfKnownTailpositions());




class Rope
{
    HashSet<string> tailPositions= new();

    Head head = new();
    Tail tail = new();

    public int NumberOfKnownTailpositions()
    {
        return tailPositions.Count;
    }
    public void MoveHead(Instruction ins)
    {
        for (int i = 0; i < ins.numberOfSteps; i++)
        {
            _=ins.dir switch
            {
                Direction.Left => head.x--,
                Direction.Right => head.x++,
                Direction.Up => head.y++,
                Direction.Down => head.y--,
            };
            MoveTail();
        }
    }

    private void MoveTail()
    {
        if(Math.Abs(head.x - tail.x) == 2 )
        {
            tail.y = head.y;
            if(head.x > tail.x)
            {
                tail.x++;
            } else
            {
                tail.x--;
            }
        } else if(Math.Abs(head.y - tail.y) == 2 )
        {
            tail.x = head.x;
            if(head.y > tail.y)
            {
                tail.y++;
            } else
            {
                tail.y--;
            }
        }
        string stringPos = $"{tail.x},{tail.y}";
        if (!tailPositions.Contains(stringPos))
        {
            tailPositions.Add(stringPos);
        }
    }

    class Head
    {
        public int x;
        public int y;
    }
    class Tail
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