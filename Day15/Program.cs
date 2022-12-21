using System.Text.RegularExpressions;

var input = File.ReadLines(Utils.Input.GetInputFilePath(15));



//input = new[] {
//"Sensor at x=2, y=18: closest beacon is at x=-2, y=15"
//,"Sensor at x=9, y=16: closest beacon is at x=10, y=16"
//,"Sensor at x=13, y=2: closest beacon is at x=15, y=3"
//,"Sensor at x=12, y=14: closest beacon is at x=10, y=16"
//,"Sensor at x=10, y=20: closest beacon is at x=10, y=16"
//,"Sensor at x=14, y=17: closest beacon is at x=10, y=16"
//,"Sensor at x=8, y=7: closest beacon is at x=2, y=10"
//,"Sensor at x=2, y=0: closest beacon is at x=2, y=10"
//,"Sensor at x=0, y=11: closest beacon is at x=2, y=10"
//,"Sensor at x=20, y=14: closest beacon is at x=25, y=17"
//,"Sensor at x=17, y=20: closest beacon is at x=21, y=22"
//,"Sensor at x=16, y=7: closest beacon is at x=15, y=3"
//,"Sensor at x=14, y=3: closest beacon is at x=15, y=3"
//,"Sensor at x=20, y=1: closest beacon is at x=15, y=3"};




var reg = new Regex(@"x=(-?\d+).+y=(-?\d+):.+x=(-?\d+).+y=(-?\d+)");

List<IPositionable> positions = new();


const int sensorXPos = 0;
const int sensorYPos = 1;
const int beaconXPos = 2;
const int beaconYPos = 3;

foreach(var l in input)
{
    var m = reg.Match(l);

    var capt = m.Groups.Values.Skip(1).Select(c => int.Parse(c.Value)).ToArray();
    var beacon = new Beacon() { X = capt[beaconXPos], Y = capt[beaconYPos] };
    var sensor = new Sensor() { X = capt[sensorXPos], Y = capt[sensorYPos], ClosestBeacon = beacon };
    positions.Add(beacon);
    positions.Add(sensor);

}

var min = FindMinimumXForY();
var max = FindMaximumXForY();

var sensors = positions.Where(pos => pos is Sensor).Cast<Sensor>().ToList();
const int rowToLookAt = 2000000;
var n = 0;
foreach (var p in Enumerable.Range(min, max - min))
{
    var inReach = sensors
        .FirstOrDefault(s => s.CalculateDistanceToPosition(new Point(p, rowToLookAt)) <= s.DistanceToClosestBeacon);

    if (inReach is not null && !positions.Any(pos => pos.X == p && pos.Y == rowToLookAt))
    {
        n++;
    }
}
Console.WriteLine(n);

var pos = FindPosition();
Console.WriteLine(pos.X * 4_000_000 + pos.Y);

Point FindPosition()
{
    for (int yPos = 0; yPos < 4_000_000; yPos++)
    {
        for (int xPos = 0; xPos < 4_000_000; xPos++)
        {
            var inReach = sensors
            .Any(s => s.CalculateDistanceToPosition(new Point(xPos, yPos)) <= s.DistanceToClosestBeacon);

            if (!inReach)
            {
                return new Point(xPos, yPos);
            }
        }
    }

    throw new Exception();
}

int FindMinimumXForY()
{
    var sensor = positions.Where(p => p is Sensor).Cast<Sensor>().MinBy(s => s.X - s.DistanceToClosestBeacon);
    return sensor!.X - sensor.DistanceToClosestBeacon;
}

int FindMaximumXForY()
{
    var sensor = positions.Where(p => p is Sensor).Cast<Sensor>().MaxBy(s => s.X + s.DistanceToClosestBeacon);
    return sensor!.X + sensor.DistanceToClosestBeacon;
}
interface IPositionable
{
    public int X { get; set; }
    public int Y { get; set; }
}

class Sensor : IPositionable
{
    private int? distanceToClosestBeacon;

    public int X { get; set; }
    public int Y { get; set; }

    public required Beacon ClosestBeacon { get; set; }

    public int DistanceToClosestBeacon
    {
        get => distanceToClosestBeacon ??= CalculateDistanceToPosition(this.ClosestBeacon);
    }

    public int CalculateDistanceToPosition(IPositionable position) =>
        Math.Abs(position.X - this.X) + Math.Abs(position.Y - this.Y);
    
}

class Beacon : IPositionable
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Point : IPositionable
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}