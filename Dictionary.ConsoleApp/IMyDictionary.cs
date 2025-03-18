
public interface IMyDictionary<TKey, UValue>
{
    UValue this[TKey key] { get; set; }

    int Count { get; }

    void Add(TKey key, UValue val);
    bool ContainsKey(TKey key);
    IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator();
}