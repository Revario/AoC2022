using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var reader = File.ReadLines("input.txt");


List<int> caloriesPerElf = new();

int calories = 0;

foreach (string line in reader)
{
    if (string.IsNullOrEmpty(line))
    {
        caloriesPerElf.Add(calories);
        calories = 0;
        continue;
    }
    calories += int.Parse(line);
}

caloriesPerElf.Sort();

Console.WriteLine($"Part1: {caloriesPerElf.Last()}");
Console.WriteLine($"Part2: {caloriesPerElf.TakeLast(3).Sum()}");


var sums = File.ReadAllText("input.txt").Split("\r\n\r\n").Select(e => e.Split(Environment.NewLine).Select(ec => int.Parse(ec)).Sum()).OrderDescending();

Console.WriteLine(sums.First());
Console.WriteLine(sums.Take(3).Sum());

BenchmarkRunner.Run<Day1>();


public class Day1
{
    [Benchmark]
    public int Part1OldStyle() {

        int maxCalories = 0;
        int elfClories = 0;

        foreach (string line in File.ReadLines("input.txt"))
        {
            if (string.IsNullOrEmpty(line))
            {
                if(elfClories > maxCalories)
                {
                    maxCalories = elfClories;
                }
                elfClories = 0;
                continue;
            }
            elfClories += int.Parse(line);
        }

        return maxCalories;
    }

    [Benchmark]
    public int Part1Linq()
    {
        return File.ReadAllText("input.txt")
            .Split("\r\n\r\n")
            .Select(e => 
                e.Split(Environment.NewLine)
                .Select(ec => int.Parse(ec))
                .Sum())
            .Max();
    }
}