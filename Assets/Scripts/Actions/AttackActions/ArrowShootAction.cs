using System;
using UnityEngine;

public class ArrowShootAction : AttackAction
{
    protected override int range => 7;

    protected override DamageTypes damageType => DamageTypes.Plant;

    protected override bool isMagical => false;

    protected override float attackValue => 50f;

    protected override bool isCirculiar => true;

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
