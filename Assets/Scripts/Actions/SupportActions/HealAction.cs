using System;
using UnityEngine;

public class HealAction : SupportAction
{
    [SerializeField]private int healAmount;

    public event EventHandler<ActionArgsWithTwoUnits> OnStartHeal;


    protected override int Range => 5;

    protected override bool IsCirculiar => true;

    public override string GetActionName()
    {
        return "Heal";
    }

    protected override void DoAction()
    {
        OnStartHeal?.Invoke(this, new ActionArgsWithTwoUnits {targetUnit = targetUnit, activeUnit = unit});
        StatSystem targetUnitStateSystem = targetUnit.GetStatSystem();
        targetUnitStateSystem.Heal(healAmount);
    }
}
