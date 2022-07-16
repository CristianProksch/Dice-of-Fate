using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionPinCollectionManager : MonoBehaviour
{
    #region Singleton
    public static ActionPinCollectionManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region ActionPinCollectionManager
    [SerializeField]
    private List<CollectionLevel> _collectionLevels;

    public  List<ActionPinCollection> getCollectionsForLevel (int level)
    {
        List<ActionPinCollection> output = new List<ActionPinCollection>();

        var collectionLevel = _collectionLevels[level];
        int[] availableCollectionLevelCount = new int[_collectionLevels.Count()];

        collectionLevel.pinsPerLevel.Aggregate(availableCollectionLevelCount, (list, item) =>
        {
            availableCollectionLevelCount[item]++;
            return availableCollectionLevelCount;
        });

        for(int i = 0; i < availableCollectionLevelCount.Count(); i++)
        {
            int ActionPinCollectionsWithLevelCount = ActionPinCollectionDB.allAvailable.Where(item => item.level == i + 1).Count();

            if (ActionPinCollectionsWithLevelCount < availableCollectionLevelCount[i])
                throw new Exception($"not enough different ActionPinCollections for level {i+1} available. in DB {ActionPinCollectionsWithLevelCount} found");
        }

        for(int i = 0; i < collectionLevel.pinsPerLevel.Count(); i++)
        {
            for (int j = 0; j < collectionLevel.pinsPerLevel[i] ; j++)
            {
                ActionPinCollection randCandidate = null;
                while (randCandidate == null || output.Contains(randCandidate))
                {
                    randCandidate = ActionPinCollectionDB.GetRandomActionPinCollection(level);
                }
                output.Add(randCandidate);
            }
        }

        return output;
    }
    #endregion
}

[Serializable]
public struct CollectionLevel{
    [SerializeField]
    public List<int> pinsPerLevel;
}
