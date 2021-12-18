using System.Text;

Console.WriteLine( "Init");
const string rootDir = "/mnt/mfs/test";
if (Directory.Exists(rootDir))
    Directory.Delete(rootDir, true);
Console.WriteLine( "Try to create folder");
Directory.CreateDirectory(rootDir);


Console.WriteLine( "Try to generate data");
byte[] data = new byte[100_000_000];
new Random().NextBytes(data);

Console.WriteLine( "Try to write first file");
File.WriteAllBytes(Path.Combine(rootDir, 0.ToString()), data);
Console.WriteLine( "Start copy cicle");
for (int i = 1; i < 400; i++)
{
    File.Copy(Path.Combine(rootDir, (i - 1).ToString()), Path.Combine(rootDir, i.ToString()));
    Console.WriteLine($"Done {i}");
}

