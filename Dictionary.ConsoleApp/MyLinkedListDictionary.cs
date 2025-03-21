using System.Collections;

public class MyLinkedListDictionary<TKey, UValue>
    : IEnumerable<KeyValuePair<TKey, UValue>>
    , IMyDictionary<TKey, UValue>
{
    private LinkedList<KeyValuePair<TKey, UValue>> _values;

    public int Count => _values.Count;

    public MyLinkedListDictionary()
    {
        _values = new LinkedList<KeyValuePair<TKey, UValue>>();
    }

    public void Add(TKey key, UValue value)
    {
        if (ContainsKey(key))
        {
            throw new Exception($"Key {key} already present");
        }
        _values.AddLast(new KeyValuePair<TKey, UValue>(key, value));
    }

    public bool ContainsKey(TKey key)
    {
        foreach (var pair in _values)
        {
            if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
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
            foreach (var pair in _values)
            {
                if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                {
                    return pair.Value;
                }
            }
            throw new Exception($"Key {key} is not present");
        }
        set
        {
            var node = _values.First;
            while (node != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(node.Value.Key, key))
                {
                    node.Value = new KeyValuePair<TKey, UValue>(key, value);
                    return;
                }
                node = node.Next;
            }
            _values.AddLast(new KeyValuePair<TKey, UValue>(key, value));
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
}
