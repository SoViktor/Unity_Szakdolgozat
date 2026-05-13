using System;
using UnityEngine;

public class ArrowShootAction : AttackAction
{
    protected override int Range => 7;

    protected override DamageTypes DamageType => DamageTypes.Plant;

    protected override bool IsMagical => false;

    protected override float AttackValue => 50f;

    protected override bool IsCirculiar => true;

    public event EventHandler<ActionArgsWithTwoUnits> OnStartArrowShootAction;


    public override string GetActionName()
    {
        return "Arrow Shoot";
    }

    protected override void DoAction()
    {

        OnStartArrowShootAction?.Invoke(this, new ActionArgsWithTwoUnits {targetUnit = targetUnit, activeUnit = unit});

        Attack();
    }

}
