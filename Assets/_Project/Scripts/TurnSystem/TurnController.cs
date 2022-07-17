using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TurnPhase
{
    MonsterPlacement,
    Placement,
    Action,
    ApplyPinPowers
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
    private MonsterController _monsterController;
    [SerializeField]
    private PlayerBehavior _player;

    [SerializeField]
    private UnityEvent _onStartMonsterPlacement;
    [SerializeField]
    private UnityEvent _onStartPlacement;
    [SerializeField]
    private UnityEvent _onStartAction;
    #endregion

    private TurnPhase _currentPhase;
    private Coroutine _applyPinsCoroutine;

    private void Initialize()
    {
        // TODO
    }

    private void Start()
    {
        GameController.AddStartCombatListener(() => StartCombat());
        GameController.AddStartUpgradingListener(() => { if (_applyPinsCoroutine != null) { StopCoroutine(_applyPinsCoroutine); _applyPinsCoroutine = null; } });
    }

    private void StartCombat()
    {
        _monsterController.SpawnMonster(GameController.GetNextMonster());
        SetPhase(TurnPhase.MonsterPlacement);
    }

    private IEnumerator ApplyPinPowers()
    {
        _player.AddArmour(_player.ArmourPower);

        yield return new WaitForSeconds(.25f);

        _monsterController.AddArmour(_monsterController.ArmourPower);

        yield return new WaitForSeconds(.25f);

        _monsterController.TakeDamage(_player.AttackPower);

        yield return new WaitForSeconds(.25f);

        _player.TakeDamage(_monsterController.AttackPower);

        yield return new WaitForSeconds(.25f);

        _player.HealDamage(_player.HealPower);

        yield return new WaitForSeconds(.25f);

        _monsterController.HealDamage(_monsterController.HealPower);

        yield return new WaitForSeconds(.25f);

        _monsterController.CheckDeath();

        yield return null;

        _player.CheckDeath();

        NextPhase();
    }

    public static void SetPhase(TurnPhase phase)
    {
        Instance._currentPhase = phase;

        switch (Instance._currentPhase)
        {
            case TurnPhase.MonsterPlacement:
                Instance._onStartMonsterPlacement?.Invoke();
                break;
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
            case TurnPhase.MonsterPlacement:
                Instance._onStartMonsterPlacement?.Invoke();
                break;
            case TurnPhase.Placement:
                Instance._onStartPlacement?.Invoke();
                break;
            case TurnPhase.Action:
                Instance._onStartAction?.Invoke();
                break;
            case TurnPhase.ApplyPinPowers:
                Instance.StartCoroutine(Instance.ApplyPinPowers());
                TutorialDisplay.SetText("");
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
