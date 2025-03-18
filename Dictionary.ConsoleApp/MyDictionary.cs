using System.Collections;

public class MyDictionary<TKey, UValue> : IEnumerable<KeyValuePair<TKey, UValue>>, IMyDictionary<TKey, UValue>
{
    private KeyValuePair<TKey, UValue>[] _items;
    private int _count;
    private const int INITIAL_CAPACITY = 4;

    public int Count => _count;

    public MyDictionary()
    {
        _items = new KeyValuePair<TKey, UValue>[INITIAL_CAPACITY];
        _count = 0;
    }

    public void Add(TKey key, UValue val)
    {
        // Check if key already exists
        if (ContainsKey(key))
        {
            throw new ArgumentException("An item with the same key has already been added.");
        }

        // Resize array if necessary
        if (_count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }

        // Add new item
        _items[_count] = new KeyValuePair<TKey, UValue>(key, val);
        _count++;
    }

    public bool ContainsKey(TKey key)
    {
        for (int i = 0; i < _count; i++)
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
            for (int i = 0; i < _count; i++)
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
            for (int i = 0; i < _count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(_items[i].Key, key))
                {
                    _items[i] = new KeyValuePair<TKey, UValue>(key, value);
                    return;
                }
            }
            // If key not found, add it
            Add(key, value);
        }
    }

    public IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    // Required for IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
