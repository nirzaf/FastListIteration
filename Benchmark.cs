using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace FastListIteration;

[MemoryDiagnoser]
public class Benchmark
{
    private static readonly Random Rng = new Random(80085);
    
    [Params(100, 100000)]
    public int Size { get; set; }
    
    private List<int> _items;
    
    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Size).Select(_ => Rng.Next()).ToList();
    }
    
    [Benchmark]
    public void ForLoop()
    {
        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
        }
    }
    
    [Benchmark]
    public void ForEach()
    {
        foreach (var item in _items) { }
    }
    
    [Benchmark]
    public void ForEachWithIndex()
    {
        foreach (var (item, _) in _items.Select((item, index) => (item, index))) { }
    }
    
    [Benchmark]
    public void ForEachWithIndexAndCount()
    {
        foreach (var (item, index) in _items.Select((item, index) => (item, index)).Take(_items.Count)) { }
    }
    
    [Benchmark]
    public void ForEachWithIndexAndCountAndLocal()
    {
        var count = _items.Count;
        foreach (var (item, index) in _items.Select((item, index) => (item, index)).Take(count)) { }
    }
    
    [Benchmark]
    public void ForEachLinq()
    {
        _items.ForEach(_ => { });
    }
    
    [Benchmark]
    public void ParallelForEach()
    {
        Parallel.ForEach(_items, _ => { });
    }
    
    [Benchmark]
    public void ParallelForEachWithIndex()
    {
        Parallel.ForEach(_items, (item, state, index) => { });
    }
    
    [Benchmark]
    public void ForEach_Span()
    {
        foreach (var item in CollectionsMarshal.AsSpan(_items))
        {
            Console.WriteLine(item); //Fastest way to iterate a list but you can't modify the list
        }
    }
}