using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionPinCollectionDB : MonoBehaviour
{
    #region Singleton
    public static ActionPinCollectionDB Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
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

    #region ActionPinCollectionDB
    [SerializeField]
    private List<ActionPinCollection> _allAvailable;

    public static List<ActionPinCollection> allAvailable { get => Instance._allAvailable; }

    public static ActionPinCollection GetRandomActionPinCollection(int level)
    {
        var selectable = allAvailable.Where(item => item.level == level).ToList();
        if (selectable.Count() == 0)
            throw new Exception($"no ActionPinCollection with level {level} found in DB");

        int index = UnityEngine.Random.Range(0, selectable.Count());

        return selectable[index];
    }
    #endregion
}
