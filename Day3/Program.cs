var lines = File.ReadAllLines(new Utils.Input("3").GetInputFilePath());


var s = lines.Sum(l =>
    GetPriority(
        l[..(l.Length / 2)]
        .Intersect(l[(l.Length / 2)..])
        .Single()
        )
);

Console.WriteLine(s);

var p = lines
    .Chunk(3)
    .Sum(l =>
        GetPriority(
            l[0]
            .Intersect(l[1])
            .Intersect(l[2])
            .Single())
);


Console.WriteLine(p);

int GetPriority(char c)
{
    return char.IsUpper(c) ? (int)c - 38 : (int)c - 96;
}