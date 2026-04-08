using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button nextTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnUI;

    private void Start() 
    {
        nextTurnButton.onClick.AddListener(() =>
        {
           TurnSystem.Instance.Nextturn(); 
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEnemyTurnUI();
        UpdateNextTurnButton();
    }

    private void UpdateTurnText()
    {
        turnNumberText.text = "TURN: " + TurnSystem.Instance.GetTurnNumber();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurnUI();
        UpdateNextTurnButton();
    }

    private void UpdateEnemyTurnUI()
    {
        enemyTurnUI.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateNextTurnButton()
    {
        nextTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }

}
