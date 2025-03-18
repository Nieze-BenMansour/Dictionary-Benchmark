using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class MyDictionaryNotOptimized<TKey, UValue> : IEnumerable<KeyValuePair<TKey, UValue>>, IMyDictionary<TKey, UValue>
{
    private List<KeyValuePair<TKey, UValue>> _values = new List<KeyValuePair<TKey, UValue>>();

    public int Count => _values.Count;

    public MyDictionaryNotOptimized()
    {
    }

    public void Add(TKey key, UValue @value)
    {
        var exists = false;
        for (var i = 0; i < _values.Count; i++)
        {
            if (_values[i].Key.Equals(key))
                exists = true;
        }
        if (!exists)
            _values.Add(new KeyValuePair<TKey, UValue>(key, @value));
        else
            throw new Exception($"key {key} already present");
    }

    public bool ContainsKey(TKey key)
    {
        var exists = false;
        for (var i = 0; i < _values.Count; i++)
        {
            if (_values[i].Key.Equals(key))
                exists = true;
        }
        return exists;
    }

    public UValue this[TKey key]
    {
        get
        {
            UValue result = default(UValue);
            for (var i = 0; i < _values.Count; i++)
            {
                if (_values[i].Key.Equals(key))
                    result = _values[i].Value;
            }
            if (!result.Equals(default(UValue)))
                return result;
            else
                throw new Exception($"key {key} is not present");
        }
        set
        {
            var exists = false;
            for (var i = 0; i < _values.Count; i++)
            {
                if (_values[i].Key.Equals(key))
                {
                    _values[i] = new KeyValuePair<TKey, UValue>(key, value);
                    exists = true;
                }
            }
            if (!exists)
                _values.Add(new KeyValuePair<TKey, UValue>(key, value));
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator()
    {
        return _values.GetEnumerator();
    }
}
