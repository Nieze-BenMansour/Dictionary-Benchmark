using System.Collections;

public class MyHashSetDictionary<TKey, UValue>
    : IEnumerable<KeyValuePair<TKey, UValue>>
    , IMyDictionary<TKey, UValue>
{
    private HashSet<KeyValuePair<TKey, UValue>> _values;
    private readonly KeyValuePairComparer _comparer;

    public int Count => _values.Count;

    public MyHashSetDictionary()
    {
        _comparer = new KeyValuePairComparer();
        _values = new HashSet<KeyValuePair<TKey, UValue>>(_comparer);
    }

    public void Add(TKey key, UValue value)
    {
        var pair = new KeyValuePair<TKey, UValue>(key, value);
        if (!_values.Add(pair))
        {
            throw new Exception($"Key {key} already present");
        }
    }

    public bool ContainsKey(TKey key)
    {
        return _values.Contains(new KeyValuePair<TKey, UValue>(key, default), _comparer);
    }

    public UValue this[TKey key]
    {
        get
        {
            foreach (var pair in _values)
            {
                if (_comparer.Equals(pair, new KeyValuePair<TKey, UValue>(key, default)))
                {
                    return pair.Value;
                }
            }
            throw new Exception($"Key {key} is not present");
        }
        set
        {
            var pair = new KeyValuePair<TKey, UValue>(key, value);
            if (_values.Remove(pair))
            {
                _values.Add(pair);
            }
            else
            {
                _values.Add(pair);
            }
        }
    }

    public IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class KeyValuePairComparer : IEqualityComparer<KeyValuePair<TKey, UValue>>
    {
        public bool Equals(KeyValuePair<TKey, UValue> x, KeyValuePair<TKey, UValue> y)
        {
            return EqualityComparer<TKey>.Default.Equals(x.Key, y.Key);
        }

        public int GetHashCode(KeyValuePair<TKey, UValue> obj)
        {
            return EqualityComparer<TKey>.Default.GetHashCode(obj.Key);
        }
    }
}
