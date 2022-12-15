using Microsoft.VisualBasic;
using System.Security.AccessControl;

var input = File.ReadAllText(Utils.Input.GetInputFilePath(13)).Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();

input = "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]".Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();



int correct = 0;
foreach(var p in input)
{
    if(UnpackAndCompare(p.Item1, p.Item2)) correct++;
}

bool UnpackAndCompare(string left, string right)
{
    if(string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right))
    {
        return false;
    }
    else if (string.IsNullOrEmpty(right))
    {
        return true;
    }

    (left, right) = Unpack(left, right);


    var l = left.Split(",");
    var r = right.Split(",");


    for (int i = 0; i < l.Length; i++)
    {
        if (l[i].StartsWith('[') && r[i].StartsWith('['))
        {
            return UnpackAndCompare(l[i], r[i]);
        }
    
        if (l[i].StartsWith('['))
        {
            return UnpackAndCompare(l[i], $"[{r[i]}]");
        } else if (r[i].StartsWith('['))
        {
            return UnpackAndCompare($"[{l[i]}]", r[i]);
        }

        int lV = ConvertToInt(l[i]);
        int rV = ConvertToInt(r[i]);

        if (lV < rV)
        {
            return true;
        } else if (lV > rV)
        {
            return false;
        }
    }
    //Compare(left, right);

    throw new ArgumentException();
}


int ConvertToInt(string inp)
{
    if(inp.Length != 1)
    {
        throw new ArgumentException();
    }

    return (int)inp[0];
}


//var (left, right) = input.First();


//for (int i = 0; i < left.Length; i++)
//{

//}


//if(Unpack(left, out var unp))
//{
//    Console.WriteLine(unp);
//}
(string, string) Unpack(string left, string right)
{
    //output = input;

    //if (string.IsNullOrEmpty(input))
    //{
    //    return false;
    //}

    //if (input.StartsWith('['))
    //{

    return (left[1..^1], right[1..^1]);

    //    return true;    
    //}

    //if(int.TryParse(input[0].ToString(), out var _))
    //{
    //    return true;
    //}

    //throw new ArgumentException("Could not unpack, unexpected input");
}

