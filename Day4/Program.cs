var inputs = File.ReadLines(new Utils.Input("4").GetInputFilePath());




var intervals = inputs.Select(i =>
{
    var intervalParts = i.Split(',')
            .SelectMany(ss => ss.Split('-').Select(n => int.Parse(n)))
            .ToArray();
    return new
    {
        firstStart = intervalParts[0],
        firstEnd = intervalParts[1],
        secondStart = intervalParts[2],
        secondEnd = intervalParts[3]
    };
}).ToArray();






Console.WriteLine(intervals.Count(a =>
    (a.firstStart <= a.secondStart
    && a.firstEnd >= a.secondEnd)
    ||
    (a.secondStart <= a.firstStart
    && a.secondEnd >= a.firstEnd)
));



Console.WriteLine(intervals.Count(a =>
    (a.firstStart <= a.secondStart
    && a.firstEnd >= a.secondStart)
    ||
    (a.secondStart <= a.firstStart
    && a.secondEnd >= a.firstStart)
));

