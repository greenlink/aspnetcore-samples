namespace ProjectSample;

internal interface IAnimalsDictionary <TKey, TValue>
{
    bool Add(TKey key, IEnumerable<TValue> values);
    IEnumerable<TValue> Get(TKey key);
    IEnumerable<TValue> GetOrDefault(TKey key);
    bool Remove(TKey key);
    bool Clear();
}
