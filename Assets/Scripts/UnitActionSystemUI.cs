using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectionChange;
        CreateUnitActionButtons();
    }
    private void CreateUnitActionButtons()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Instantiate(actionButtonPrefab, actionButtonContainerTransform);
        }
    }

    private void UnitActionSystem_OnSelectionChange(object sender, EventArgs e) 
    {
        CreateUnitActionButtons();    
    }
}
