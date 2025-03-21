using System.Collections;

public class MyArrayDictionary<TKey, UValue> : IEnumerable<KeyValuePair<TKey, UValue>>, IMyDictionary<TKey, UValue>
{
    private KeyValuePair<TKey, UValue>[] _items;
    private const int INITIAL_CAPACITY = 4;

    public int Count { get; private set; }

    public MyArrayDictionary()
    {
        _items = new KeyValuePair<TKey, UValue>[INITIAL_CAPACITY];
        Count = 0;
    }

    public void Add(TKey key, UValue val)
    {
        if (ContainsKey(key))
        {
            throw new ArgumentException("An item with the same key has already been added.");
        }

        if (Count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }

        _items[Count] = new KeyValuePair<TKey, UValue>(key, val);
        Count++;
    }

    public bool ContainsKey(TKey key)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(_items[i].Key, key))
            {
                return true;
            }
        }
        return false;
    }

    public UValue this[TKey key]
    {
        get
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(_items[i].Key, key))
                {
                    return _items[i].Value;
                }
            }
            throw new KeyNotFoundException($"The key '{key}' was not found in the dictionary.");
        }
        set
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(_items[i].Key, key))
                {
                    _items[i] = new KeyValuePair<TKey, UValue>(key, value);
                    return;
                }
            }

            Add(key, value);
        }
    }

    public IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
