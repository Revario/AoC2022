
var instructions = File.ReadAllLines(new Utils.Input("7").GetInputFilePath());

var topDir = new Directory() { Name = "/" };
Directory.AllDirectories.Add(topDir);

var currentDir = topDir;


foreach(var inst in instructions)
{
    _ = inst.Split(" ") switch
    {
        [_, "cd", "/"] => currentDir = topDir,
        [_, "cd", ".."] => currentDir = currentDir.Parent,
        [_, "cd", var folderName] => currentDir = currentDir.AddSubdirectory(folderName),
        [var sizeStr, var name] when int.TryParse(sizeStr, out int size) => currentDir.AddFile(name, size),
        _ => new Directory()
    };
};


Console.WriteLine(Directory.AllDirectories.Where(d => d.Size <= 100_000).Sum(d => d.Size));

var freeSpace = 70_000_000 - topDir.Size;
var additionalSpaceNeeded = 30_000_000 - freeSpace;

Console.WriteLine(
    Directory.AllDirectories
            .Where(d => d.Size >= additionalSpaceNeeded)
            .OrderBy(d => d.Size)
            .Select(d => d.Size)
            .First());





class Directory
{
    public static List<Directory> AllDirectories { get; set; } = new();
    public Directory Parent { get; private set; } = default!;
    public string Name { get; set; } = default!;

    private int? _size;
    public int? Size
    {
        get => _size ??= CalculateSize();
        private set
        {
            _size = value;
        }
    }
    public List<Directory> SubDirectories { get; private set; } = new();

    public List<FileInfo> Files { get; private set; } = new();

    public Directory AddSubdirectory(string name)
    {
        var dir = new Directory() { Name = name, Parent = this};
        SubDirectories.Add(dir);
        AllDirectories.Add(dir);
        return dir;
    }

    public Directory AddFile(string name, int size)
    {
        Files.Add(new(name, size));

        return this;
    }

    private int? CalculateSize()
    {
        return Files.Sum(f => f.size) + SubDirectories.Sum(d => d.Size);
    }

}


record FileInfo (string Name, int size);