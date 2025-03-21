using System.Collections;

public class MySpanDictionary<TKey, UValue>
    : IEnumerable<KeyValuePair<TKey, UValue>>,
    IMyDictionary<TKey, UValue>
{
    private HashSet<KeyValuePair<TKey, UValue>>[] _buckets;
    private int _count;
    private const int INITIAL_CAPACITY = 4;

    public int Count => _count;

    public MySpanDictionary()
    {
        _buckets = new HashSet<KeyValuePair<TKey, UValue>>[INITIAL_CAPACITY];
        _count = 0;
    }

    private int GetBucketIndex(TKey key)
    {
        int hash = key?.GetHashCode() ?? 0; 
        return Math.Abs(hash) % _buckets.Length;
    }

    private void Resize()
    {
        int newCapacity = _buckets.Length * 2;
        var newBuckets = new HashSet<KeyValuePair<TKey, UValue>>[newCapacity];

        var allItems = new KeyValuePair<TKey, UValue>[_count];
        int itemIndex = 0;
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var item in bucket)
                {
                    allItems[itemIndex++] = item;
                }
            }
        }

        Span<KeyValuePair<TKey, UValue>> itemsSpan = allItems.AsSpan();
        for (int i = 0; i < itemsSpan.Length; i++)
        {
            var item = itemsSpan[i];
            int newIndex = Math.Abs(item.Key?.GetHashCode() ?? 0) % newCapacity;
            if (newBuckets[newIndex] == null)
            {
                newBuckets[newIndex] = new HashSet<KeyValuePair<TKey, UValue>>();
            }
            newBuckets[newIndex].Add(item);
        }

        _buckets = newBuckets;
    }

    public void Add(TKey key, UValue val)
    {
        if (ContainsKey(key))
        {
            throw new ArgumentException("An item with the same key has already been added.");
        }

        if (_count >= _buckets.Length * 0.75)
        {
            Resize();
        }

        int index = GetBucketIndex(key);
        if (_buckets[index] == null)
        {
            _buckets[index] = new HashSet<KeyValuePair<TKey, UValue>>();
        }

        _buckets[index].Add(new KeyValuePair<TKey, UValue>(key, val));
        _count++;
    }

    public bool ContainsKey(TKey key)
    {
        int index = GetBucketIndex(key);
        if (_buckets[index] == null)
        {
            return false;
        }

        foreach (var item in _buckets[index])
        {
            if (EqualityComparer<TKey>.Default.Equals(item.Key, key))
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
            int index = GetBucketIndex(key);
            if (_buckets[index] != null)
            {
                foreach (var item in _buckets[index])
                {
                    if (EqualityComparer<TKey>.Default.Equals(item.Key, key))
                    {
                        return item.Value;
                    }
                }
            }
            throw new KeyNotFoundException($"The key '{key}' was not found in the dictionary.");
        }
        set
        {
            int index = GetBucketIndex(key);
            if (_buckets[index] != null)
            {
                var existingPair = new KeyValuePair<TKey, UValue>(key, default(UValue));
                foreach (var item in _buckets[index])
                {
                    if (EqualityComparer<TKey>.Default.Equals(item.Key, key))
                    {
                        existingPair = item;
                        break;
                    }
                }
                if (_buckets[index].Contains(existingPair))
                {
                    _buckets[index].Remove(existingPair);
                    _buckets[index].Add(new KeyValuePair<TKey, UValue>(key, value));
                    return;
                }
            }
            Add(key, value);
        }
    }

    public IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator()
    {
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var item in bucket)
                {
                    yield return item;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}