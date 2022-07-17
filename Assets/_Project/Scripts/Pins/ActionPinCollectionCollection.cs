using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionPinCollectionCollection : MonoBehaviour
{
    private List<ActionPinCollection> _availableCollections = new List<ActionPinCollection>();
    private List<ActionPinCollection> _placedCollections = new List<ActionPinCollection>();

    public List<ActionPinCollection> GetRandomActionPinCollections (int count)
    {
        if (count >= _availableCollections.Count)
        {
            return new List<ActionPinCollection>(_availableCollections);
        }
        
        var random = new System.Random();
        return _availableCollections.OrderBy(item => random.Next()).Take(count).ToList();
    }

    public void RemoveActionPinCollectionPermanently(ActionPinCollection toRemove)
    {
        _availableCollections.Remove(toRemove);
        _placedCollections.Remove(toRemove);
    }

    public void SetActionPinCollectionPlaced(ActionPinCollection collection)
    {
        if (_availableCollections.Contains(collection))
        {
            _availableCollections.Remove(collection);
            _placedCollections.Add(collection);
        }
        else
        {
            Debug.Log("Dayum son! You shouldn't be here");
        }
    }

    public void Add(ActionPinCollection toAdd)
    {
        _availableCollections.Add(toAdd);
    }

    public void AddRange(IEnumerable<ActionPinCollection> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public void ResetAvailability()
    {
        foreach(var collection in _placedCollections)
        {
            Add(collection);
        }

        _placedCollections.Clear();
    }
}
