using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    [SerializeField] private Transform actionPointPrefab;
    [SerializeField] private Transform actionPointContainerTransform;

    private List<ActionButtonUI> actionButtonUIList;
    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectedUnitChange;
        UnitActionSystem.Instance.OnSelectedActionChange += UnitActionSystem_OnSelectedActionChange;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoint();
    }
    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);

            actionButtonUIList.Add(actionButtonUI);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs e) 
    {
        CreateUnitActionButtons();  
        UpdateSelectedVisual();
        UpdateActionPoint();
    }
    private void UnitActionSystem_OnSelectedActionChange(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoint();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateActionPoint();
    }

    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoint()
    {

        foreach (Transform actionPointTransform in actionPointContainerTransform)
        {
            Destroy(actionPointTransform.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        for (int i = 0; i < selectedUnit.GetActionPoints(); i++)
        {
            Instantiate(actionPointPrefab, actionPointContainerTransform);
        }
        return;
    }

}
