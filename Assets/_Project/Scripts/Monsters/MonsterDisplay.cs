using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDisplay : MonoBehaviour
{
    #region Inspector
    [Header("References")]
    [SerializeField]
    private MonsterController _monsterController;

    [Space(5)]
    [Header("Display References")]
    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private Image _monsterImage;
    [SerializeField]
    private Image _healthFill;
    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private TextMeshProUGUI _attackText;
    [SerializeField]
    private TextMeshProUGUI _defenseText;
    [SerializeField]
    private TextMeshProUGUI _healText;
    #endregion

    private void Update()
    {
        if (GameController.GetCurrentPhase() == GamePhase.Upgrading || _monsterController._currentMonster == null)
        {
            Hide();
        }
        else
        {
            UpdateContent();
            Show();
        }
    }

    private void Show()
    {
        _container.SetActive(true);
    }

    private void Hide()
    {
        _container.SetActive(false);
    }

    private void UpdateContent()
    {
        _monsterImage.sprite = _monsterController.MonsterSprite;

        _healthText.text = $"{_monsterController.CurrentMonsterHealth} / {_monsterController.MaxMonsterHealth}";
        _healthFill.fillAmount = (float)_monsterController.CurrentMonsterHealth / (float)_monsterController.MaxMonsterHealth;

        _attackText.text = _monsterController.AttackPower.ToString();
        _defenseText.text = _monsterController.ArmourPower.ToString();
        _healText.text = _monsterController.HealPower.ToString();
    }
}
