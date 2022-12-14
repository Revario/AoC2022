

public class Program
{
    public static void Main(string[] args)
    {
        var inputPart1 = File.ReadAllText(Utils.Input.GetInputFilePath(13))
            .Split("\n\n")
            .Select(s => s.Split("\n"))
            .Select(i => (new Signal(i[0]), new Signal(i[1])))
            //.Take(15)
            .ToList();

        //input = "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]".Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();


        List<int> correctPairs = new();
        for (int i = 0; i < inputPart1.Count; i++)
        {
            var curInp = inputPart1[i];

            var res = CompareLists(curInp.Item1.S, curInp.Item2.S);
            if (res == -1)
            {
                correctPairs.Add(i + 1);
            }

            if(res == 0)
            {
                throw new Exception("Could not make descision");
            }

        }

        Console.WriteLine(correctPairs.Sum());

        var inputPart2 = File.ReadAllLines(Utils.Input.GetInputFilePath(13))
           .Where(l => !string.IsNullOrWhiteSpace(l))
           .Select(s => new Signal(s))
           //.Take(15)
           .ToList();

        Signal divider2 = new("[[2]]");
        Signal divider6 = new("[[6]]");

        inputPart2.AddRange(new Signal[]{ divider2, divider6 });

        inputPart2.Sort();

        var divider2Index = inputPart2.FindIndex(s => s == divider2) + 1;
        var divider6Index = inputPart2.FindIndex(s => s == divider6) + 1;


        Console.WriteLine(divider2Index * divider6Index);
    }

    public record Signal(string S) : IComparable<Signal>
    {
        public int CompareTo(Signal? other)
        {
            return CompareLists(this.S, other.S);
        }
    }
    public static int CompareLists(string lInp, string rInp)
    {
        var curL = Unpack(lInp);
        var curR = Unpack(rInp);

        for (int l = 0; l < curL.Length; l++)
        {
            if (l >= curR.Length)
            {
                break;
            }

            var left = curL[l];
            var right = curR[l];

            if (IsList(left) || IsList(right))
            {
                var c = CompareLists(left, right);
                if (c != 0)
                {
                    return c;
                }
                continue;
            }

            var comp = Compare(left, right);

            if (comp != 0)
            {
                return comp;
            }
        }
        if (curL.Length != curR.Length)
        {
            return curL.Length.CompareTo(curR.Length);
        }
        return 0;
    }


    public static int Compare(string lInp, string rInp)
    {
        var l = int.Parse(lInp);
        var r = int.Parse(rInp);
        return l.CompareTo(r);
    }
    public static bool IsList(string input)
    {
        return input.StartsWith("[");
    }

    public static string[] Unpack(string inp)
    {
        List<string> data = new();

        if (!inp.StartsWith('['))
        {
            var comma = inp.IndexOf(',');
            inp = inp.Insert(comma > 0 ? comma : inp.Length, "]");
            inp = inp.Insert(0, "[");
        }

        var endOfList = FindEndPositionForList(inp);
        var toProcess = inp[1..endOfList];


        var nesting = 0;
        for (int i = 0, prev = 0; i < toProcess.Length; i++)
        {
            if (toProcess[i] == '[') nesting++;
            if (toProcess[i] == ']') nesting--;
            if (toProcess[i] == ',' && nesting == 0)
            {
                data.Add(toProcess[prev..i]);
                prev = i + 1;
            }
            if (i == toProcess.Length - 1)
            {
                data.Add(toProcess[prev..]);
            }
        }

        var rest = inp[(FindEndPositionForList(inp) + 1)..];
        if (!string.IsNullOrEmpty(rest))
        {
            data.Add(rest);
        }

        return data.ToArray();
    }


    public static int FindEndPositionForList(string l)
    {
        if (l[0] != '[')
        {
            throw new ArgumentException();

        }
        int nesting = 0;

        for (int i = 0; i < l.Length; i++)
        {
            if (l[i] == '[') nesting++;
            if (l[i] == ']') nesting--;

            if (nesting == 0)
            {
                return i;
            }
        }

        throw new ArgumentException("Could not find list end");
    }
}