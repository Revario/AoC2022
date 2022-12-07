
using System.Reflection.Metadata.Ecma335;

var instructions = File.ReadAllLines(new Utils.Input("7").GetInputFilePath());


var topDir = new Directory() { Name = "/" };
var currentDir = topDir;


foreach(var inst in instructions)
{
    _ = inst.Split(" ") switch
    {
        ["cd", "/"] => currentDir = topDir,
        ["cd", ".."] => currentDir = currentDir.Parent,
        ["cd", var folderName] => currentDir = currentDir.AddSubdirectory(folderName),
        [var size, var name] when int.TryParse(size, out int g) == true => currentDir.AddFile(name, g),
        _ => new Directory()
    };
};


Console.WriteLine(currentDir);

class Directory
{
    public Directory Parent { get; private set; }
    public string Name { get; set; }
    public List<Directory> SubDirecories { get; private set; } = new();

    public List<FileInfo> Files { get; private set; } = new();

    public Directory AddSubdirectory(string name)
    {
        var dir = new Directory() { Name = name, Parent = this};
        SubDirecories.Add(dir);

        return dir;
    }

    public Directory AddFile(string name, int size)
    {
        Files.Add(new(name, size));

        return this;
    }

}


record FileInfo (string Name, int size);