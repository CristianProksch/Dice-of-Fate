using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    #region Inspector
    [Header("References")]
    [SerializeField]
    private PlayerBehavior _player;

    [Space(5)]
    [Header("Display References")]
    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private Image _playerImage;
    [SerializeField]
    private Image _healthFill;
    [SerializeField]
    private Image _manaFill;
    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private TextMeshProUGUI _attackText;
    [SerializeField]
    private TextMeshProUGUI _defenseText;
    [SerializeField]
    private TextMeshProUGUI _healText;
    [SerializeField]
    private TextMeshProUGUI _manaText;
    #endregion

    private void Update()
    {
        if (GameController.GetCurrentPhase() == GamePhase.Upgrading)
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
        _playerImage.sprite = _player.PlayerSprite;

        _healthText.text = $"{_player.CurrentHealth} / {_player.MaxHealth}";
        _manaText.text = $"{_player.CurrentMana} / {_player.MaxMana}";
        _healthFill.fillAmount = (float)_player.CurrentHealth / (float)_player.MaxHealth;
        _manaFill.fillAmount = (float)_player.CurrentMana / (float)_player.MaxMana;

        _attackText.text = _player.AttackPower.ToString();
        _defenseText.text = _player.ArmourPower.ToString();
        _healText.text = _player.HealPower.ToString();
    }
}
