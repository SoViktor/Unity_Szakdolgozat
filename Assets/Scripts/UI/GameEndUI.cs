using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private GameObject playerWinUI;
    [SerializeField] private GameObject playerLoseUI;

    private void Start()
    {
        GameEndSystem.Instance.OnAnyTeamWin += GameEndSystem_OnAnyTeamWin;
        HideGameEndUI();

    }

    private void Update()
    {
        if (Keyboard.current.enterKey.IsPressed())
        {
            ShowPlayerWinUI();
        }
        if (Keyboard.current.enterKey.IsPressed())
        {
            HideGameEndUI();
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

    private void GameEndSystem_OnAnyTeamWin(object sender, EventArgs e)
    {
        bool hasPlayerWon = GameEndSystem.Instance.DidPlayerWin();


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
        GameEndSystem.Instance.OnAnyTeamWin -= GameEndSystem_OnAnyTeamWin;
    }


}
