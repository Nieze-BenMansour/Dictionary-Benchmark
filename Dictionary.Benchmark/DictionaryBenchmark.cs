using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class DictionaryBenchmark
{
    private MyArrayDictionary<string, string> _myArrayDict;
    private MyDictionaryNotOptimized<string, string> _myDictNotOptimized;
    private MyHashSetDictionary<string, string> _myHashSetDict;
    private MyLinkedListDictionary<string, string> _myLinkedListDict;
    private MySpanDictionary<string, string> _mySpanDict;
    private Dictionary<string, string> _stdDict;
    private string[] _keys;

    [Params(100, 1000, 10000)]
    public int N { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _myArrayDict = new MyArrayDictionary<string, string>();
        _myDictNotOptimized = new MyDictionaryNotOptimized<string, string>();
        _myHashSetDict = new MyHashSetDictionary<string, string>();
        _myLinkedListDict = new MyLinkedListDictionary<string, string>();
        _mySpanDict = new MySpanDictionary<string, string>();
        _stdDict = new Dictionary<string, string>();
        _keys = new string[N];

        for (int i = 0; i < N; i++)
        {
            string key = Guid.NewGuid().ToString();
            _keys[i] = key;

            _myArrayDict.Add(key, key);
            _myDictNotOptimized.Add(key, key);
            _myHashSetDict.Add(key, key);
            _myLinkedListDict.Add(key, key);
            _mySpanDict.Add(key, key);
            _stdDict.Add(key, key);
        }
    }

    // *** ADD Operations ***
    [Benchmark]
    public void MyArrayDictionary_Add()
    {
        var dict = new MyArrayDictionary<string, string>();
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
    public void MyHashSetDictionary_Add()
    {
        var dict = new MyHashSetDictionary<string, string>();
        for (int i = 0; i < N; i++)
            dict.Add(_keys[i], _keys[i]);
    }

    [Benchmark]
    public void MyLinkedListDictionary_Add()
    {
        var dict = new MyLinkedListDictionary<string, string>();
        for (int i = 0; i < N; i++)
            dict.Add(_keys[i], _keys[i]);
    }

    [Benchmark]
    public void MySpanDictionary_Add()
    {
        var dict = new MySpanDictionary<string, string>();
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

    // *** Lookup Operations ***
    [Benchmark]
    public string MyArrayDictionary_Lookup()
    {
        return _myArrayDict[_keys[N / 2]];
    }

    [Benchmark]
    public string MyDictionaryNotOptimized_Lookup()
    {
        return _myDictNotOptimized[_keys[N / 2]];
    }

    [Benchmark]
    public string MyHashSetDictionary_Lookup()
    {
        return _myHashSetDict[_keys[N / 2]];
    }

    [Benchmark]
    public string MyLinkedListDictionary_Lookup()
    {
        return _myLinkedListDict[_keys[N / 2]];
    }

    [Benchmark]
    public string MySpanDictionary_Lookup()
    {
        return _mySpanDict[_keys[N / 2]];
    }

    [Benchmark]
    public string StdDictionary_Lookup()
    {
        return _stdDict[_keys[N / 2]];
    }
}