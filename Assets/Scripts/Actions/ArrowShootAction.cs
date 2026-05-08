using System;
using UnityEngine;

public class ArrowShootAction : AttackAction
{
    protected override int maxAttackDistance => 7;

    protected override DamageTypes damageType => DamageTypes.Plant;

    protected override bool isMagical => false;

    protected override float attackValue => 50f;

    protected override bool isAttackCirculiar => true;

    public event EventHandler<ActionArgsWithTwoUnits> OnStartArrowShootAction;


    public override string GetActionName()
    {
        return "Arrow Shoot";
    }

    protected override void Attack()
    {
        SetAttackStat();

        OnStartArrowShootAction?.Invoke(this, new ActionArgsWithTwoUnits {targetUnit = targetUnit, activeUnit = unit});

        targetUnit.Damage(isMagical, damageType, attackValue, attackStat );
    }
}
