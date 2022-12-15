var input = File.ReadAllText(Utils.Input.GetInputFilePath(13)).Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();

input = "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]".Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();

Console.WriteLine(input);

List<int> correct = new();


for (int i = 0; i < input.Count; i++)
{
    var (left, right) = input[i];

    left = ReplaceChars(left);
    right = ReplaceChars(right);

    //var leftParts = left.Split(",");
    //var rightParts = right.Split(",");


    for (int l = 0; l < left.Length; l++)
    {
        var lchar = left[l];

        if(l >= right.Length)
        {
            break;
        }
        var rchar = right[l];

        if(lchar == '#')
        {
            correct.Add(i + 1);
            break;
        }
        
        if (lchar == '#')
        {
            break;
        }

        if ((int)lchar > (int)rchar)
        {
            break;
        }
        if ((int)lchar < (int)rchar)
        {
            correct.Add(i + 1);
            break;
        }

        if(l == left.Length - 1)
        {
            correct.Add(i + 1);
        }
    }
}

Console.WriteLine(correct.Sum());
//Console.WriteLine(correct.Aggregate((prev, next) => prev * next));

string ReplaceChars(string input)
{
    return input.Replace("[]", "#").Replace("[", string.Empty).Replace("]", string.Empty).Replace(",", string.Empty);
}



//for (int signalNum = 0; signalNum < input.Count; signalNum++)
//{
//	var (left, right) = input[signalNum];

//    var rightCounter = 0;
//    for (int i = 0; i < left.Length; i++, rightCounter++)
//    {
//        if (i + 1 < left.Length && left[i] == '[' && left[i + 1] == ']')
//        {
//            correct.Add(i);
//            break;
//        }

//        if (left[i] == '[')

//    }
//}




//class SignalPair
//{


//}


//class Signal
//{
//    public string signal { get; set; }
//}