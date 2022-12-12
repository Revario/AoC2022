var input = File.ReadAllLines(Utils.Input.GetInputFilePath(12));

var mapHeight = input.Length;
var mapWidth = input.First().Length;


char[,] map = new char[mapWidth, mapHeight];

Position current = default!;
Position goal = default!;
HashSet<Position> knownPositions = new();
Queue<Position> toExplore = new();


for (int y = 0; y < input.Length; y++)
{
	for (int x = 0; x < input.First().Length; x++)
	{
		var p = input[y][x];

		if(p == 'S')
		{
			current = new Position(x, y);
            map[x, y] = 'a';
        }
        else if(p == 'E')
		{
			goal = new Position(x, y);
			map[x, y] = 'z';
		} else
		{
			map[x, y] = p;
		}
	}
}

int steps = 0;

knownPositions.Add(current);
toExplore.Enqueue(current);

while (!Explore())
{
	current = toExplore.Dequeue();
}

Console.WriteLine(knownPositions.First(p => p == goal));


bool Explore()
{
	Position[] newPos = {
	new Position(current.x + 1, current.y),
	new Position(current.x - 1, current.y),
	new Position(current.x, current.y + 1),
	new Position(current.x, current.y - 1)
};

	foreach(var p in newPos.Where(IsInsideBoundary))
	{
		if (!knownPositions.Contains(p) && map[p.x, p.y] <= map[current.x, current.y] + 1)
		{
			p.steps = current.steps + 1;
			knownPositions.Add(p);
			if (p == goal) return true;
			toExplore.Enqueue(p);
		}
	}

	return false;
}

bool IsInsideBoundary(Position p) => !(p.x >= mapWidth || p.x < 0 || p.y >= mapHeight || p.y < 0);

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