using Microsoft.VisualBasic;
using System.Security.AccessControl;

var input = File.ReadAllText(Utils.Input.GetInputFilePath(13)).Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();

input = "[1,1,3,1,1]\n[1,1,5,1,1]\n\n[[1],[2,3,4]]\n[[1],4]\n\n[9]\n[[8,7,6]]\n\n[[4,4],4,4]\n[[4,4],4,4,4]\n\n[7,7,7,7]\n[7,7,7]\n\n[]\n[3]\n\n[[[]]]\n[[]]\n\n[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]".Split("\n\n").Select(s => s.Split("\n")).Select(i => (i[0], i[1])).ToList();



int correct = 0;

for (int s = 0; s < input.Count; s++)
{
	var l = input[s].Item1;
	var r = input[s].Item2;

	while (true) {
		if (string.IsNullOrEmpty(l))
		{
			correct += s + 1;
			break;
		} else if (string.IsNullOrEmpty(r))
		{
			break;
		}



		//if (l[li] == '[')
		//{
		//	if (r[ri] == '[')
		//	{
		//		continue;
		//	} else
		//	{
		//		r = r.Insert(ri + 1, "]");
		//              li++;
		//	}
		//} else if (r[ri] == '[')
		//{
		//	l = l.Insert(li + 1, "]");
		//	ri++;
		//}

		//if (l[li] == ']') 
		//{
		//	if (r[ri] == ']')
		//	{
		//		continue;
		//	} else
		//	{
		//              correct += s + 1;
		//		break;
		//	}
		//} else if (r[ri] == ']')
		//{
		//	break;
		//}



		//TODO hantera slutbracket när enskild siffra wrappas

		(l, r) = Unpack(l, r);

		if (l[0] != ']' && r[0] == ']')
		{
			break;
		}

		if (l[0] < r[0] || l[0] == ']')
		{
			correct += s + 1;
			break;
		}
		
		if (l[0] > r[0])
		{
			break;
		}

        l = l[1..];
        r = r[1..];

    }


    
}

Console.WriteLine(correct);


//bool Unpack(string l, string r, out string left, out string right)
//{
//    if (l[0] == '[')
//    {
//        if (r[0] == '[')
//        {
//			left = l[1..];
//			right = r[1..];

//            return false;
//        }
//        else
//        {
//			left = l[1..];
//			right = r.Insert(1, "]");
//            return false;
//        }
//    }
//    else if (r[0] == '[')
//    {
//        left = l.Insert(1, "]");
//		right = r[1..];
//		return false;
//    }

//    if (l[0] == ']' && r[0] == ']')
//    {
//		left = l[1..];
//		right = r[1..];
//		return false;
//    }
//	left = l;
//	right = r;
//	return true;
//}

(string left, string right) Unpack(string l, string r)
{
	if (l.Length > 2 && l[..2] == "[]")
	{
		return (l[1..], r[1..]);
	}
    if (l[0] == '[')
    {
        if (r[0] == '[')
        {
            return Unpack(l[1..], r[1..]);
        }
        else
        {
            return Unpack(l[1..], r.Insert(1, "]"));
        }
    }
    else if (r[0] == '[')
    {
        return Unpack(l.Insert(1, "]"), r[1..]);
    }

    if (l[0] == ']' && r[0] == ']')
    {
        return Unpack(l[1..], r[1..]);
    }
    return (l, r);
}