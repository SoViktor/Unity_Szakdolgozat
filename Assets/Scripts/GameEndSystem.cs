using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEndSystem : MonoBehaviour
{
    public static GameEndSystem Instance {get; private set;}

    public event EventHandler OnAnyTeamWin;


    private bool hasPlayerWon;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("To Many GameEndSystems! Only one Allowed" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        TurnSystem.Instance.OnFinishedUpdateOnTurnOrder += TurnSystem_OnFinishedUpdateOnTurnOrder;
    }


    private void TurnSystem_OnFinishedUpdateOnTurnOrder(object sender, EventArgs e)
    {
        List<Unit> unitList = TurnSystem.Instance.GetTurnOrderList();
        bool hasEnemyAnyMember = false;
        bool hasPlayerAnyMember = false;

        foreach (Unit item in unitList)
        {
            if (item == null)
            {
                continue;
            }

            if (item.IsEnemy())
            {
                hasEnemyAnyMember = true;
            }
            else
            {
                hasPlayerAnyMember = true;
            }
        }

        if (hasEnemyAnyMember && hasPlayerAnyMember)
        {
            return;
        }

        hasPlayerWon = hasPlayerAnyMember;
        OnAnyTeamWin?.Invoke(this, EventArgs.Empty);

    }

    public bool DidPlayerWin()
    {
        return hasPlayerWon;
    }

    private void OnDestroy()
    {
        TurnSystem.Instance.OnFinishedUpdateOnTurnOrder -= TurnSystem_OnFinishedUpdateOnTurnOrder;

    }
}
