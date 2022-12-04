var inputs = File.ReadLines(new Utils.Input("4").GetInputFilePath());




var s = inputs.Select(i =>
{
    var a = i.Split(',').SelectMany(ss => ss.Split('-')).ToArray();
    return new
    {
        firstStart = int.Parse(a[0]),
        firstEnd = int.Parse(a[1]),
        secondStart = int.Parse(a[2]),
        secondEnd = int.Parse(a[3])
    };
}).ToArray();






Console.WriteLine(s.Count(a =>
    (a.firstStart <= a.secondStart
    && a.firstEnd >= a.secondEnd)
    ||
    (a.secondStart <= a.firstStart
    && a.secondEnd >= a.firstEnd)
));



Console.WriteLine(s.Count(a =>
    (a.firstStart <= a.secondStart
    && a.firstEnd >= a.secondStart)
    ||
    (a.secondStart <= a.firstStart
    && a.secondEnd >= a.firstStart)
));

