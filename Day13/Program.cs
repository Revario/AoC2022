

public class Program
{
    public static void Main(string[] args)
    {
        var input = File.ReadAllText(Utils.Input.GetInputFilePath(13)).Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();

        //input = "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]".Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();


        List<int> correctPairs = new();
        for (int i = 0; i < input.Count; i++)
        {
            var curInp = input[i];

            if (CompareLists(curInp.Item1, curInp.Item2) == 1)
            {
                correctPairs.Add(i + 1);
            }

        }

        Console.WriteLine(correctPairs.Sum());

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

            if (comp < 0)
            {
                return 1;
            }
            if (comp > 0)
            {
                return -1;
            }
        }
        if (curL.Length != curR.Length)
        {
            return curR.Length.CompareTo(curL.Length);
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
            inp = inp.Insert(comma > 0 ? comma : inp.Length - 1, "]");
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