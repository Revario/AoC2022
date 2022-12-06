using System.Text.RegularExpressions;

var input = File.ReadAllText(new Utils.Input("6").GetInputFilePath());

Console.WriteLine(FindPositionRegexPart1(input));

Console.WriteLine(FindPositionClassic(input, 4));
Console.WriteLine(FindPositionClassic(input, 14));


Console.WriteLine(FindPositionWithLinq(input, 4));
Console.WriteLine(FindPositionWithLinq(input, 14));


int FindPositionWithLinq(string input, int numUnique)
{
    for(int i = 0; i < input.Length - numUnique; i++)
    {
        if(input[i..(i + numUnique)].Distinct().Count() == numUnique)
        {
            return i + numUnique;
        }
    }

    throw new Exception("Not found");
}
int FindPositionClassic(string input, int numUnique)
{
    for(int i = 0; i < input.Length - numUnique; i++)
    {
        List<char> prevChars = new();
        prevChars.Add(input[i]);

        for(int n = 1; n < numUnique; n++)
        {
            if (prevChars.Contains(input[i + n]))
            {
                break;
            }
            prevChars.Add(input[i + n]);

            if(prevChars.Count == numUnique)
            {
                return i + numUnique;
            }
        }
    }

    throw new Exception("Sequence not found");

}

int FindPositionRegexPart1(string input)
{
    var match = new Regex(@"([a-z])(?!.{0,2}\1)([a-z])(?!.{0,1}\2)([a-z])(?!\3)([a-z])", RegexOptions.Compiled)
        .Match(input);

    return match.Index + 4;

}