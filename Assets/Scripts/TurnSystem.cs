using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance {get; private set;}

    public event EventHandler OnTurnChanged;
    private int turnNumber = 1;


    private List<Unit> unitList;
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
        unitList = new List<Unit>();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Unit");

        foreach (GameObject item in gameObjects)
        {
            Unit unit = item.GetComponent<Unit>();
            if (unit != null)
            {
                unitList.Add(unit);
            }
        }

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
        activeUnit = nextUnit;
        activeUnit.ResetActionPoints();

        UnitActionSystem.Instance.SetSelectedUnit(activeUnit);

        turnNumber++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    private Unit GetUnitWithLowestActionValue()
    {
        return unitList
            .OrderBy(unit => unit.GetStatSystem().GetActionValue())
            .ThenBy(unit => unit.GetStatSystem().GetBaseRandom())
            .First();
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

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

}
