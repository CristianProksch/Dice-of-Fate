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
    private List<ActionPinCollectionByPlayerLevelCount> _collectionLevels;

    public  List<ActionPinCollection> getCollectionsForPlayerLevel (int level)
    {
        List<ActionPinCollection> output = new List<ActionPinCollection>();

        var collectionLevel = _collectionLevels[level-1];
        /* int[] collectionLevelsCount = new int[_collectionLevels.Count()];

        collectionLevel.ActionPinCollectionByLevelCount.Aggregate(collectionLevelsCount, (list, item) =>
        {
            collectionLevelsCount[item]++;
            return collectionLevelsCount;
        });

        for(int i = 0; i < collectionLevelsCount.Count(); i++)
        {
            int availableActionPinCollectionsCount = ActionPinCollectionDB.allAvailable.Where(item => item.level == i + 1).Count();

            if (availableActionPinCollectionsCount < collectionLevelsCount[i])
                throw new Exception($"not enough different ActionPinCollections for level {i+1} available. in DB {availableActionPinCollectionsCount} found");
        }
        */
        
        foreach(var actionPinCollectionByLevelCount in collectionLevel.ActionPinCollectionByLevelCount)
        {
            for(int i = 0; i < actionPinCollectionByLevelCount; i++)
            {
                ActionPinCollection randCandidate = null;
                //while (randCandidate == null || output.Contains(randCandidate))
                //{
                randCandidate = ActionPinCollectionDB.GetRandomActionPinCollection(level);
                //}
                output.Add(randCandidate);
            }
        }

        return output;
    }
    #endregion
}

[Serializable]
public struct ActionPinCollectionByPlayerLevelCount{
    [SerializeField]
    public List<int> ActionPinCollectionByLevelCount;
}
