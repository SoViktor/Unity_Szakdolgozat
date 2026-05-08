using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform singelTurnOrderTransform;
    [SerializeField]private int MaxShowedUnits = 7;

    private void Start()
    {

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyUnitDied += Unit_OnAnyUnitDied;

        if (TryGetComponent<SummonAction>(out SummonAction summonAction))
        {
            summonAction.OnNewUnitSummoned += SummonAction_OnNewUnitSummoned;
        }

        UpdateVisual();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    //They dont work. Only update visual when turn changed.

    private void Unit_OnAnyUnitDied(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void SummonAction_OnNewUnitSummoned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform item in container)
        {

           Destroy(item.gameObject);

        }

            List<Unit> turnOrderUnits = TurnSystem.Instance.GetTurnOrderList();

            int counter = 0;


            foreach (Unit item in turnOrderUnits)
            {
                if (counter >= MaxShowedUnits)
                {
                    break;
                }
                Transform turnOrderTransform = Instantiate(singelTurnOrderTransform, container);

                turnOrderTransform.gameObject.SetActive(true);

                TurnOrderSingleUI turnOrderSingleUI = turnOrderTransform.GetComponent<TurnOrderSingleUI>();

                turnOrderSingleUI.SetUnit(item);
                counter++;

            }
        
    }

    private void OnDestroy()
    {
        TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;
        Unit.OnAnyUnitDied -= Unit_OnAnyUnitDied;

    }
}
