using System.Security.Cryptography.X509Certificates;

var input = File.ReadLines(Utils.Input.GetInputFilePath(14));
//input = new[] { "498,4 -> 498,6 -> 496,6", "503,4 -> 502,4 -> 502,9 -> 494,9" };

var inp = input
	.Select(i => i.Split(" -> ")
		.Select(d =>
		{
			var g = d.Split(",").Select(i => int.Parse(i));
			return new Coordinate(g.First(), g.Last());
		})
		.ToList()
	).ToList();

HashSet<Coordinate> map = new HashSet<Coordinate>();

foreach (var l in inp)
{

	for (int i = 0; i < l.Count - 1; i++)
	{
		AddLineToMap(l[i], l[i + 1]);
	}
}

var bottom = map.MaxBy(c => c.Y).Y;
var numSand = 0;
bool part1Done = false;
while (true)
{
	var s = new Sand(new(500, 0), map, bottom);

	var c = s.Move();
	map.Add(c);

	if(c.Y > bottom && !part1Done)
	{
        Console.WriteLine(numSand);
		part1Done = true;
	}
    if (c == new Coordinate(500, 0))
    {
        Console.WriteLine(numSand+1);
		break;
    }
    numSand++;
}



//PrintMap(map);

void AddLineToMap(Coordinate start, Coordinate end)
{
	HashSet<Coordinate> line;

    if(start.X != end.X)
	{
		line = Enumerable.Range(0, Math.Abs(end.X - start.X) + 1)
            .Select(x => new Coordinate(start.X < end.X ? start.X + x : start.X - x, start.Y))
            .ToHashSet();
	} else
	{
        line = Enumerable.Range(0, Math.Abs(end.Y - start.Y) + 1)
            .Select(y => new Coordinate(start.X, start.Y < end.Y ? start.Y + y : start.Y - y))
            .ToHashSet();
    }
    
	map.UnionWith(line);
}

void PrintMap(HashSet<Coordinate> map)
{
	var heightStart = map.MinBy(c => c.Y).Y;
	var heightStop = map.MaxBy(c => c.Y).Y;
	var widthStart = map.MinBy(c => c.X).X;
	var widthStop = map.MaxBy(c => c.X).X;

	for (int y = heightStart; y <= heightStop; y++)
	{
		for (int x = widthStart; x <= widthStop; x++)
		{
			Console.Write(map.Contains(new Coordinate(x, y)) ? "#" : ".");
		}
		Console.WriteLine();
	}
}

public class Sand
{
	HashSet<Coordinate> map;
	int bottom;
	public Coordinate c { get; private set; }
    public Sand(Coordinate start, HashSet<Coordinate> map, int bottom)
    {
        this.c = start;
        this.map = map;

		this.bottom = bottom;
    }

    public Coordinate Move()
	{
		while (true)
		{
			var down = new Coordinate(c.X, c.Y + 1);
			var left = new Coordinate(c.X - 1, c.Y + 1);
			var right = new Coordinate(c.X + 1, c.Y + 1);


			if(c.Y > bottom)
			{
				return c;
			}

			if (!map.Contains(down))
			{
				c = down;
			} 
			else if (!map.Contains(left))
			{
				c = left;
			} 
			else if (!map.Contains(right))
			{
				c = right;
			} 
			else
			{
				return c;
			}
		}
    }
}

public record Coordinate(int X, int Y);