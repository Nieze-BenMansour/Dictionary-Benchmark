using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<DictionaryBenchmark>();
    }
}

[MemoryDiagnoser]
public class DictionaryBenchmark
{
    private MyDictionary<string, string> _myDict;
    private MyDictionaryNotOptimized<string, string> _myDictNotOptimized;
    private Dictionary<string, string> _stdDict;
    private string[] _keys;

    [Params(10, 100, 1000)]
    public int N { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _myDict = new MyDictionary<string, string>();
        _myDictNotOptimized = new MyDictionaryNotOptimized<string, string>();
        _stdDict = new Dictionary<string, string>();
        _keys = new string[N];

        for (int i = 0; i < N; i++)
        {
            string key = Guid.NewGuid().ToString();
            _keys[i] = key;
            _myDict.Add(key, key);
            _myDictNotOptimized.Add(key, key);
            _stdDict.Add(key, key);
        }
    }

    [Benchmark]
    public void MyDictionary_Add()
    {
        var dict = new MyDictionary<string, string>();
        for (int i = 0; i < N; i++)
            dict.Add(_keys[i], _keys[i]);
    }

    [Benchmark]
    public void MyDictionaryNotOptimized_Add()
    {
        var dict = new MyDictionaryNotOptimized<string, string>();
        for (int i = 0; i < N; i++)
            dict.Add(_keys[i], _keys[i]);
    }

    [Benchmark]
    public void StdDictionary_Add()
    {
        var dict = new Dictionary<string, string>();
        for (int i = 0; i < N; i++)
            dict.Add(_keys[i], _keys[i]);
    }

    [Benchmark]
    public string MyDictionary_Lookup()
    {
        return _myDict[_keys[N / 2]];
    }

    [Benchmark]
    public string MyDictionaryNotOptimized_Lookup()
    {
        return _myDictNotOptimized[_keys[N / 2]];
    }

    [Benchmark]
    public string StdDictionary_Lookup()
    {
        return _stdDict[_keys[N / 2]];
    }
}
