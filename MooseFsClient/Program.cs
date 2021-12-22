using System.Drawing;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

Summary? summary = BenchmarkRunner.Run<FileSystemBenchmark>();

[SimpleJob(RunStrategy.Monitoring, RuntimeMoniker.Net60, 1, 0, 1, 10)]

[AsciiDocExporter]
[CsvExporter]
[CsvMeasurementsExporter]
[MarkdownExporterAttribute.GitHub]
[JsonExporterAttribute.Brief]
public class FileSystemBenchmark
{
    public const string RootDir = "/mnt/mfs/test";
    public const string SampleRoot = "/mnt/mfs/sample";

    private byte[] _data;
    private readonly Random _random = new Random();

    public long BlockCount => 1_000 / BlockSize;
    //public long BlockCount => 1_000_000_000 / BlockSize;

    [Params(100, 100_000, 100_000_000)]
    public long BlockSize;

    [GlobalSetup]
    public void ReadBlockInit()
    {
        Console.WriteLine("Start global init");
        int[] sizes = new[] { 100, 100_000, 100_000_00 };

        if (Directory.Exists(SampleRoot))
            Directory.Delete(SampleRoot, true);
        Directory.CreateDirectory(SampleRoot);

        foreach (int size in sizes)
        {
            Console.WriteLine($"Start global init for {size}");

            var data = new byte[size];
            _random.NextBytes(data);
            File.WriteAllBytes(Path.Combine(SampleRoot, size.ToString()), data);
        }
        
    }

    [IterationSetup]
    public void Init()
    {
        Console.WriteLine("Start iteration init");

        if (Directory.Exists(RootDir))
            Directory.Delete(RootDir, true);
        Directory.CreateDirectory(RootDir);
        _data = new byte[BlockSize];
        _random.NextBytes(_data);

        Console.WriteLine("End iteration init");
    }

    [Benchmark]
    public void Read()
    {
        for (var i = 0; i < BlockCount; i++)
        {
            File.ReadAllBytes(Path.Combine(SampleRoot, BlockSize.ToString()));
        }
    }

    [Benchmark]
    public void ReadParallel()
    {
        Enumerable
            .Range(1, (int)BlockCount)
            .AsParallel()
            .ForAll(i => File.ReadAllBytes(Path.Combine(SampleRoot, BlockSize.ToString())));
    }

    [Benchmark]
    public void ReadWrite()
    {
        for (var i = 0; i < BlockCount; i++)
        {
            File.WriteAllBytes(Path.Combine(RootDir, i.ToString()), _data);
            File.ReadAllBytes(Path.Combine(RootDir, i.ToString()));
        }
    }

    [Benchmark]
    public void ReadWriteParallel()
    {
        Enumerable
            .Range(1, (int)BlockCount)
            .AsParallel()
            .ForAll(i =>
            {
                File.WriteAllBytes(Path.Combine(RootDir, i.ToString()), _data);
                File.ReadAllBytes(Path.Combine(RootDir, i.ToString()));
            });
    }

    [Benchmark]
    public void Write()
    {
        for (var i = 0; i < BlockCount; i++)
        {
            File.WriteAllBytes(Path.Combine(RootDir, i.ToString()), _data);
        }
    }

    [Benchmark]
    public void WriteParallel()
    {
        Enumerable
            .Range(1, (int)(BlockCount))
            .AsParallel()
            .ForAll(i => File.WriteAllBytes(Path.Combine(RootDir, i.ToString()), _data));
    }

    [Benchmark]
    public void Copy()
    {
        string sourceFile = Path.Combine(RootDir, 0.ToString());
        File.WriteAllBytes(sourceFile, _data);
        for (var i = 1; i < BlockCount; i++)
        {
            File.Copy(sourceFile, Path.Combine(RootDir, i.ToString()));
        }
    }

    [Benchmark]
    public void CopyParallel()
    {
        string sourceFile = Path.Combine(RootDir, 0.ToString());
        File.WriteAllBytes(sourceFile, _data);

        Enumerable
            .Range(1, (int)(BlockCount - 1))
            .AsParallel()
            .ForAll(i => File.Copy(sourceFile, Path.Combine(RootDir, i.ToString())));
    }
}