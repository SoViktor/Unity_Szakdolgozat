using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance {get; private set;}

    public event EventHandler OnTurnChanged;
    private int turnNumber = 1;

    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("To Many TurnSystems! Only one Allowed" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Nextturn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke(this,EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
        
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
