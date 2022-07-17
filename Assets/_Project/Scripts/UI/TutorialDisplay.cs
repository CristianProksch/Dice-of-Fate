using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    #region Singleton
    public static TutorialDisplay Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField]
    private TextMeshProUGUI _tutorialText;

    public static void SetText(string instruction)
    {
        Instance._tutorialText.text = instruction;
    }
}
