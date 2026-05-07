using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private GameObject playerWinUI;
    [SerializeField] private GameObject playerLoseUI;

    private void Start()
    {
        TurnSystem.Instance.OnAnyTeamWin += TurnSystem_OnAnyTeamWin;
        HideGameEndUI();

    }

    private void Update()
    {
        if (Keyboard.current.tKey.IsPressed())
        {
            ShowPlayerWinUI();
        }
    }

    private void HideGameEndUI()
    {
        playerLoseUI.SetActive(false);
        playerWinUI.SetActive(false);
    }

    private void ShowPlayerWinUI()
    {
        playerWinUI.SetActive(true);
    }

    private void ShowPlayerLoseUI()
    {
        
        playerLoseUI.SetActive(true);
    }

    private void TurnSystem_OnAnyTeamWin(object sender, EventArgs e)
    {
        bool hasPlayerWon = TurnSystem.Instance.DidPlayerWin();

        Debug.Log(hasPlayerWon);

        if (hasPlayerWon)
        {
            ShowPlayerWinUI();
        }else
        {
            ShowPlayerLoseUI();
        }
    }

    private void OnDestroy()
    {
        TurnSystem.Instance.OnAnyTeamWin -= TurnSystem_OnAnyTeamWin;
    }


}
