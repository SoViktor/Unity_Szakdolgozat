using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float waitTime;

    private float timer;

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }
    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        Unit activeUnit = TurnSystem.Instance.GetActiveUnit();

        if (activeUnit == null)
        {
            return;
        }

        if (!activeUnit.IsEnemy())
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TurnSystem.Instance.Nextturn();
        }

    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        timer = waitTime;
    }
}
