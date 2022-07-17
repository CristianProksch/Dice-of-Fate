using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GamePhase
{
    Combat,
    Upgrading
}

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Initialize();
        }
    }
    #endregion

    #region Inspector
    [Header("Settings")]
    [SerializeField]
    private List<MonsterData> _monsters;
    [SerializeField]
    private List<ActionPinCollection> _playerStartCollections;
    [SerializeField]
    private int _targetFrameRate;

    [Space(5)]
    [Header("Scene References")]
    [SerializeField]
    private PlayerBehavior _player;

    [Space(5)]
    [Header("Events")]
    [SerializeField]
    private UnityEvent _onStartCombat;
    [SerializeField]
    private UnityEvent _onStartUpgrading;
    #endregion

    private GamePhase _currentPhase;
    private Dictionary<int, MonsterData> _monsterForTurn;
    private int _currentMonsterId;

    private void Initialize()
    {
        _monsterForTurn = new Dictionary<int, MonsterData>();
        int counter = 0;
        foreach(MonsterData monster in _monsters)
        {
            _monsterForTurn.Add(counter++, monster);
        }
        _currentMonsterId = -1;

        _player._actionPinCollectionCollection.AddRange(_playerStartCollections);

        Application.targetFrameRate = _targetFrameRate;
    }

    private IEnumerator Start()
    {
        // TODO think of a better way when and where to start the game
        yield return null;
        SetPhase(GamePhase.Combat);
    }

    public static void SetPhase(GamePhase phase)
    {
        Instance._currentPhase = phase;

        switch (Instance._currentPhase)
        {
            case GamePhase.Combat:
                Instance._onStartCombat?.Invoke();
                break;
            case GamePhase.Upgrading:
                Instance._onStartUpgrading?.Invoke();
                break;
        }
    }

    public static void NextPhase()
    {
        var temp = (int)Instance._currentPhase;
        temp = ++temp % Enum.GetValues(typeof(GamePhase)).Length;

        Instance._currentPhase = (GamePhase)temp;

        switch (Instance._currentPhase)
        {
            case GamePhase.Combat:
                Instance._onStartCombat?.Invoke();
                break;
            case GamePhase.Upgrading:
                Instance._onStartUpgrading?.Invoke();
                break;
        }
    }

    public static GamePhase GetCurrentPhase()
    {
        return Instance._currentPhase;
    }

    public static MonsterData GetNextMonster()
    {
        if (Instance._currentMonsterId >= Instance._monsters.Count)
        {
            Debug.Log("You won, but I can't handle that");
        }

        return Instance._monsterForTurn[++Instance._currentMonsterId];
    }

    public static int GetCurrentLevel()
    {
        return Instance._currentMonsterId + 1;
    }

    public static void OnMonsterHasDied()
    {
        NextPhase();
    }

    public static void AddStartCombatListener(UnityAction listener)
    {
        Instance._onStartCombat.AddListener(listener);
    }

    public static void RemoveStartCombatListener(UnityAction listener)
    {
        Instance._onStartCombat.RemoveListener(listener);
    }

    public static void AddStartUpgradingListener(UnityAction listener)
    {
        Instance._onStartUpgrading.AddListener(listener);
    }

    public static void RemoveStartUpgradingListener(UnityAction listener)
    {
        Instance._onStartUpgrading.RemoveListener(listener);
    }
}
