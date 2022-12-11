var input = File.ReadAllText(Utils.Input.GetInputFilePath(11));



var monkeyInput = input.Split("\n\n");

Console.WriteLine(monkeyInput[0]);



class Monkey
{
    public List<int> Items { get; set; }
    public Func<int, int> Operation { get; set; }
    public Func<int, int> Test { get; set; }
}