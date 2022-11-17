using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSample;

internal class AnimalsDictionary : IAnimalsDictionary<string, string>
{
    private IDictionary<string, HashSet<string>> _animalDictionary;

    public AnimalsDictionary()
    {
        _animalDictionary = new Dictionary<string, HashSet<string>>();
    }

    public bool Add(string key, IEnumerable<string> values)
    {
        if (_animalDictionary.ContainsKey(key))
        {
            if (!_animalDictionary.TryGetValue(key, out var result))
                return false;

            if (result == null)
                return false;

            var newValues = ExtractDiffernt(result, values);

            if (newValues.Count() <= 0)
                return false;

            return AddToExistingKeyValuePair(key, newValues);
        }

        return AddNewKeyValuePair(key, values);
    }

    private IEnumerable<string> ExtractDiffernt(HashSet<string> oldValues, IEnumerable<string> newValues)
    {
        var extractedList = new List<string>();
        
        foreach(var newValue in newValues)
        {
            if(!oldValues.Contains(newValue))
                extractedList.Add(newValue);
        }

        return extractedList;
    }

    private bool AddNewKeyValuePair(string key, IEnumerable<string> values)
    {
        try
        {
            _animalDictionary.Add(key, values.ToHashSet());
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    private bool AddToExistingKeyValuePair(string key, IEnumerable<string> values)
    {
        try
        {
            _ = values.All(value => _animalDictionary[key].Add(value));            
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public bool Clear()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> Get(string key)
    {
        return _animalDictionary[key];
    }

    public IEnumerable<string> GetOrDefault(string key)
    {
        if (_animalDictionary.TryGetValue(key, out var value)) 
        {
            if (value == null)
                return Enumerable.Empty<string>();
            else
                return value;
        }
        else
            return Enumerable.Empty<string>();
    }

    public bool Remove(string key)
    {
        throw new NotImplementedException();
    }
}
