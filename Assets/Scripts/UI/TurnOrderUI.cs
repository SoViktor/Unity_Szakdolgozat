using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform singelTurnOrderTransform;

    private void Start()
    {

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyUnitDied += Unit_OnAnyUnitDied;

        UpdateVisual();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void Unit_OnAnyUnitDied(object sender, EventArgs e)
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

            foreach (Unit item in turnOrderUnits)
            {
                Transform turnOrderTransform = Instantiate(singelTurnOrderTransform, container);

                turnOrderTransform.gameObject.SetActive(true);

                TurnOrderSingleUI turnOrderSingleUI = turnOrderTransform.GetComponent<TurnOrderSingleUI>();

                turnOrderSingleUI.SetUnit(item);

            }
        
    }

    private void OnDestroy()
    {
        TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;
        Unit.OnAnyUnitDied -= Unit_OnAnyUnitDied;

    }
}
