using BenchmarkDotNet.Attributes;
using BenchmarkDotNet;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Running;

//var input = File.ReadAllText(new Utils.Input("6").GetInputFilePath());

//Console.WriteLine(FindPositionRegexPart1(input));

//Console.WriteLine(FindPositionClassic(input, 4));
//Console.WriteLine(FindPositionClassic(input, 14));


//Console.WriteLine(FindPositionWithLinq(input, 4));
//Console.WriteLine(FindPositionWithLinq(input, 14));

BenchmarkRunner.Run<Day6Bench>();
public partial class Day6Bench {

    string input;

    [GlobalSetup]
    public void Setup()
    {
        input = File.ReadAllText(new Utils.Input("6").GetInputFilePath());
    }


    [Params(4, 14)]
    public int numUnique;


    [Benchmark]
    public int FindPositionWithLinq()
    {
        for (int i = 0; i < input.Length - numUnique; i++)
        {
            if (input[i..(i + numUnique)].Distinct().Count() == numUnique)
            {
                return i + numUnique;
            }
        }

        throw new Exception("Not found");
    }

    [Benchmark]
    public int FindPositionClassic()
    {
        for (int i = 0; i < input.Length - numUnique; i++)
        {
            List<char> prevChars = new();
            prevChars.Add(input[i]);

            for (int n = 1; n < numUnique; n++)
            {
                if (prevChars.Contains(input[i + n]))
                {
                    break;
                }
                prevChars.Add(input[i + n]);

                if (prevChars.Count == numUnique)
                {
                    return i + numUnique;
                }
            }
        }

        throw new Exception("Sequence not found");

    }

    [GeneratedRegex(@"([a-z])(?!.{0,2}\1)([a-z])(?!.{0,1}\2)([a-z])(?!\3)([a-z])", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
    private static partial Regex Part1Regex();

    [Benchmark]
    public int FindPositionRegexPart1SourceGenerated()
    {
        var match = Part1Regex().Match(input);

        return match.Index + 4;

    }


    public static Regex RegexInterpreted = new Regex(@"([a-z])(?!.{0,2}\1)([a-z])(?!.{0,1}\2)([a-z])(?!\3)([a-z])");
    [Benchmark]
    public int FindPositionRegexPart1Classic()
    {
        var match = RegexInterpreted.Match(input);

        return match.Index + 4;

    }


    public static Regex RegexPrecompiled = new Regex(@"([a-z])(?!.{0,2}\1)([a-z])(?!.{0,1}\2)([a-z])(?!\3)([a-z])", RegexOptions.Compiled);
    [Benchmark]
    public int FindPositionRegexPart1ClassicPrecompiled()
    {
        var match = RegexPrecompiled.Match(input);

        return match.Index + 4;

    }
}
