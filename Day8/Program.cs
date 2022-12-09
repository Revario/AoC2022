var input = File.ReadAllLines(new Utils.Input("8").GetInputFilePath());

//input = new string[]{"30373",
//"25512",
//"65332",
//"33549",
//"35390" };

Tree[,] trees = new Tree[input.Length, input.Length];


List<List<Tree>> treesByRow = new();
List<List<Tree>> treesByColumn = new();

for(int i = 0; i < input.Length; i++)
{
    treesByRow.Add(new List<Tree>());
    for(int n = 0; n < input.Length; n++)
    {
        if(treesByColumn.Count <= n)
        {
            treesByColumn.Add(new List<Tree>());
        }

        var height = int.Parse(input[i][n].ToString());
        var visible = i == 0 || i == input.Length - 1 || n == 0 || n == input.Length - 1;
        var tree = new Tree() { Height = height, Visible = visible };
        trees[i, n] = tree;

        treesByRow[i].Add(tree);
        treesByColumn[n].Add(tree);
    }
}


SetTreeVisibility(treesByRow);
SetTreeVisibility(treesByColumn);


int sum = 0;
foreach(var t in trees)
{
    if (t.Visible)
    {
        sum++;
    }
}
Console.WriteLine(sum.ToString());




for (int i = 0; i < treesByRow.Count; i++)
{
    for (int n = 0; n < treesByColumn.Count; n++)
    {
        var nnn = CalculateScoreForDirection(trees[i, n], treesByRow[i].ToArray()[(n+1)..]);

        var reverseAra2 = treesByRow[i].ToArray()[..n];
        Array.Reverse(reverseAra2);
        var nn2 = CalculateScoreForDirection(trees[i, n], reverseAra2);

        var reverseAra = treesByColumn[n].ToArray()[..i];
        Array.Reverse(reverseAra);
        var nn3 = CalculateScoreForDirection(trees[i, n], reverseAra);
        var nn4 = CalculateScoreForDirection(trees[i, n], treesByColumn[n].ToArray()[(i + 1)..]);

        trees[i, n].ScenicScore = nnn * nn2 * nn3 * nn4;
    }
}

int max = 0;
foreach(var t in trees)
{
    if(t.ScenicScore > max) max = t.ScenicScore;
}
Console.WriteLine(max);

void SetTreeVisibility(List<List<Tree>> treesa)
{
    foreach (var r in treesa)
    {
        var ar = r.ToArray();
        for (int i = 0; i < r.Count; i++)
        {
            if (r[i].Visible) { continue; }
            r[i].Visible = ar[..i].All(t => t.Height < r[i].Height) || ar[(i+1)..].All(t => t.Height < r[i].Height);
        }
    }
}

int CalculateScoreForDirection(Tree source, Tree[] trees)
{
    for (int i = 0; i < trees.Length; i++)
    {
        if (trees[i].Height >= source.Height)
        {
            return i + 1;
        }
    }
    return trees.Length;
}
class Tree
{
    public bool Visible { get; set; }
    public int Height { get; set; } = -1;

    public int ScenicScore { get; set; }

    public override string ToString()
    {
        return $"{Height.ToString()} {Visible.ToString()}";
    }
}