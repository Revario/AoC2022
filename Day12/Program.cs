var input = File.ReadAllLines(Utils.Input.GetInputFilePath(12));

var mapHeight = input.Length;
var mapWidth = input.First().Length;
Position current = default!;
Position goal = default!;

List<Position> possibleStarts = new();

char[,] map = new char[mapWidth, mapHeight];

for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input.First().Length; x++)
    {
        var p = input[y][x];

        if (p == 'S')
        {
            current = new Position(x, y);
            map[x, y] = 'a';
            possibleStarts.Add(current);
        }
        else if (p == 'E')
        {
            goal = new Position(x, y);
            map[x, y] = 'z';
        }
        else
        {
            map[x, y] = p;
        }
        if(p == 'a')
        {
            possibleStarts.Add(new(x,y));
        }
    }
}

var part1 = new PathFinder(map, current, goal);
//part1.Parse(input);
Console.WriteLine(part1.FindShortestPath());

Console.WriteLine(possibleStarts.Select(s => new PathFinder(map, s, goal).FindShortestPath()).Where(i => i>=0).Min());


class PathFinder
{
    int mapHeight;
    int mapWidth;

    Position current = default!;
    Position goal = default!;
    HashSet<Position> knownPositions = new();
    Queue<Position> toExplore = new();
    char[,]? map;


    //public void Parse(string[] input)
    //{

    //    mapHeight = input.Length;
    //    mapWidth = input.First().Length;
    //    //Position part1Start = default!;
    //    //Position goal = default!;

    //    map = new char[mapWidth, mapHeight];

    //    for (int y = 0; y < input.Length; y++)
    //    {
    //        for (int x = 0; x < input.First().Length; x++)
    //        {
    //            var p = input[y][x];

    //            if (p == 'S')
    //            {
    //                current = new Position(x, y);
    //                map[x, y] = 'a';
    //            }
    //            else if (p == 'E')
    //            {
    //                goal = new Position(x, y);
    //                map[x, y] = 'z';
    //            }
    //            else
    //            {
    //                map[x, y] = p;
    //            }
    //        }
    //    }
    //    knownPositions.Add(current);
    //    toExplore.Enqueue(current);
    //}

    public PathFinder(char[,] map, Position start, Position goal)
    {
        this.mapWidth = map.GetLength(0);
        this.mapHeight = map.GetLength(1);
        this.map = map;

        this.current = start;
        this.goal = goal;

    }

    public int FindShortestPath()
    {
        while (!Explore())
        {
            if(!toExplore.TryDequeue(out current!))
            {
                return -1;
            }
        }

        return knownPositions.First(p => p.Equals(goal)).steps;
    }


    bool Explore()
    {
        Position[] newPos = {
            new Position(current.x + 1, current.y),
            new Position(current.x - 1, current.y),
            new Position(current.x, current.y + 1),
            new Position(current.x, current.y - 1)
        };

        foreach (var p in newPos.Where(IsInsideBoundary))
        {
            if (!knownPositions.Contains(p) && map[p.x, p.y] <= map[current.x, current.y] + 1)
            {
                p.steps = current.steps + 1;
                knownPositions.Add(p);
                if (p.Equals(goal)) return true;
                toExplore.Enqueue(p);
            }
        }

        return false;
    }

    bool IsInsideBoundary(Position p) => !(p.x >= mapWidth || p.x < 0 || p.y >= mapHeight || p.y < 0);

}

class Position
{
    public int x { get; set; }
    public int y { get; set; }

    public int steps { get; set; }

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Position(int x, int y, int steps) : this(x, y)
    {
        this.steps = steps;
    }
    public override bool Equals(object? obj)
    {
        return obj is Position position &&
               y == position.y && x == position.x;
    }

    public override int GetHashCode()
    {
        return (x, y).GetHashCode();
    }
}