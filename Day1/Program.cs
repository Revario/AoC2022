var reader = File.ReadLines(new Utils.Input("1", "").GetInputFilePath());


List<int> caloriesPerElf = new ();

int calories = 0;

foreach(string line in reader)
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