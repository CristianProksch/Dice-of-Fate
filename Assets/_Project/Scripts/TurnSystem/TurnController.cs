using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TurnPhase
{
    MonsterPlacement,
    Placement,
    Action
}

public class TurnController : MonoBehaviour
{
    #region Singleton
    public static TurnController Instance { get; private set; }

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
    [SerializeField]
    private UnityEvent _onStartMonsterPlacement;
    [SerializeField]
    private UnityEvent _onStartPlacement;
    [SerializeField]
    private UnityEvent _onStartAction;
    #endregion

    private TurnPhase _currentPhase;

    private void Initialize()
    {
        // TODO
    }

    private IEnumerator Start()
    {
        // TODO think of a better way when and where to start the game
        yield return null;
        SetPhase(TurnPhase.Placement);
    }

    public static void SetPhase(TurnPhase phase)
    {
        Instance._currentPhase = phase;

        switch (Instance._currentPhase)
        {
            case TurnPhase.Placement:
                Instance._onStartPlacement?.Invoke();
                break;
            case TurnPhase.Action:
                Instance._onStartAction?.Invoke();
                break;
        }
    }

    public static void NextPhase()
    {
        var temp = (int)Instance._currentPhase;
        temp = ++temp % Enum.GetValues(typeof(TurnPhase)).Length;

        Instance._currentPhase = (TurnPhase)temp;

        switch (Instance._currentPhase)
        {
            case TurnPhase.Placement:
                Instance._onStartPlacement?.Invoke();
                break;
            case TurnPhase.Action:
                Instance._onStartAction?.Invoke();
                break;
        }
    }

    public static TurnPhase GetCurrentPhase()
    {
        return Instance._currentPhase;
    }

    public static void AddStartMonsterPlacementListener(UnityAction listener)
    {
        Instance._onStartMonsterPlacement.AddListener(listener);
    }

    public static void RemoveStartMonsterPlacementListener(UnityAction listener)
    {
        Instance._onStartMonsterPlacement.RemoveListener(listener);
    }

    public static void AddStartPlacementListener(UnityAction listener)
    {
        Instance._onStartPlacement.AddListener(listener);
    }

    public static void RemoveStartPlacementListener(UnityAction listener)
    {
        Instance._onStartPlacement.RemoveListener(listener);
    }

    public static void AddStartActionListener(UnityAction listener)
    {
        Instance._onStartAction.AddListener(listener);
    }

    public static void RemoveStartActionListener(UnityAction listener)
    {
        Instance._onStartAction.RemoveListener(listener);
    }
}
