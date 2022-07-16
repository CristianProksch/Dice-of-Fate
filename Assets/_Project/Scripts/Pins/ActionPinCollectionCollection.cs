using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionPinCollectionCollection : MonoBehaviour
{
    [SerializeField]
    private List<(ActionPinCollection collection, bool isAvailable)> _allActionPinCollections = new List<(ActionPinCollection collection, bool isAvailable)>();

    public List<ActionPinCollection> GetRandomActionPinCollections (int count)
    {
        List<ActionPinCollection> drawCollection = new List<ActionPinCollection>();
        for(int i = 0; i < count; i++)
        {
            List<ActionPinCollection> availableCollections = (List<ActionPinCollection>)_allActionPinCollections.Where(item => item.isAvailable == true).Select(item => item.collection);

            if(availableCollections.Count < 1)
            {
                return drawCollection;
            }

            int index = Random.Range(0, availableCollections.Count);
            drawCollection.Add(availableCollections[index]);
            var toRemoveElement = _allActionPinCollections[index];

            _allActionPinCollections[index] = (toRemoveElement.collection, false);
        }
        return drawCollection;
    }

    public bool RemoveActionPinCollection(ActionPinCollection toRemove, bool removePermanently=false)
    {
        if (_allActionPinCollections.Remove((toRemove, true)))
            return true;

        return _allActionPinCollections.Remove((toRemove, false));
    }

    public void Add(ActionPinCollection toAdd)
    {
        _allActionPinCollections.Add((toAdd,true));
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
        for(int i = 0; i < _allActionPinCollections.Count; i++)
        {
            _allActionPinCollections[i] = (_allActionPinCollections[i].collection,true);
        }
    }
}
