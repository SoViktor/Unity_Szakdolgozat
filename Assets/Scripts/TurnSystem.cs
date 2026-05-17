using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance {get; private set;}

    public event EventHandler OnTurnChanged;
    public event EventHandler OnFinishedUpdateOnTurnOrder;
    private int turnNumber = 1;

    private readonly int actionValuePerTurn = 100;
    private int actionValueUntilNextTurn = 100;

    private List<Unit> unitList = new();
    private Unit activeUnit;


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
    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Unit");

        foreach (GameObject item in gameObjects)
        {
            if (item.TryGetComponent<Unit>(out var unit))
            {
                unitList.Add(unit);
            }
        }

        Unit.OnAnyUnitDied += Unit_OnAnyUnitDied;
        FindNextActiveUnit();
    }
    public void Nextturn()
    {
        if (activeUnit != null)
        {
            activeUnit.GetStatSystem().ResetActionValue();
        }
        FindNextActiveUnit();
    }
    private void FindNextActiveUnit()
    {
        unitList.RemoveAll(unit => unit == null);

        if (unitList.Count == 0)
        {
            return;
        }

        Unit nextUnit = GetUnitWithLowestActionValue();

        int lowestActionValue = nextUnit.GetStatSystem().GetActionValue();

        foreach (Unit item in unitList)
        {
            item.GetStatSystem().DecreaseActionValue(lowestActionValue);
        }

        if (actionValueUntilNextTurn > lowestActionValue)
        {
            actionValueUntilNextTurn -= lowestActionValue;
        }
        else
        {
            while (actionValueUntilNextTurn < lowestActionValue)
            {
                turnNumber++;
                actionValueUntilNextTurn += actionValuePerTurn;
            }
            actionValueUntilNextTurn -= lowestActionValue;
        }
        activeUnit = nextUnit;
        activeUnit.ResetActionPoints();

        UnitActionSystem.Instance.SetSelectedUnit(activeUnit);

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    private void ReorderUnitList()
    {
        unitList = unitList
        .Where(unit => unit != null)
        .OrderBy(unit => unit.GetStatSystem().GetActionValue())
        .ThenBy(unit => unit.GetStatSystem().GetBaseRandom())
        .ToList();
    }

    private Unit GetUnitWithLowestActionValue()
    {
        ReorderUnitList();
        return unitList.First();
    }

    public Unit GetActiveUnit()
    {
        return activeUnit;
    }


    public int GetTurnNumber()
    {
        return turnNumber;
        
    }

    public bool IsPlayerTurn()
    {
        if (activeUnit == null)
        {
            return false;
        }
        return !activeUnit.IsEnemy();
    }

    public List<Unit> GetTurnOrderList()
    {
        ReorderUnitList();
        return unitList;
    }

    private void Unit_OnAnyUnitDied(object sender, EventArgs e)
    {
        Unit deadUnit = sender as Unit;

        if (deadUnit != null)
        {
            unitList.Remove(deadUnit);
        }
        ReorderUnitList();
        OnFinishedUpdateOnTurnOrder?.Invoke(this, EventArgs.Empty);

    }



    public void AddUnitToTurnSystem(Unit unit)
    {
        if (unit == null)
        {
            return;
        }

        if (!unitList.Contains(unit))
        {
            unitList.Add(unit);
        }
    }

}
