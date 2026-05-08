using System;
using UnityEngine;

public class ArrowShootAction : AttackAction
{
    protected override int maxAttackDistance => 7;

    protected override DamageTypes damageType => DamageTypes.Plant;

    protected override bool isMagical => false;

    protected override float attackValue => 50f;

    protected override bool isAttackCirculiar => true;

    public event EventHandler<OnStartArrowShootActionArgs> OnStartArrowShootAction;

    public class OnStartArrowShootActionArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit attackingUnit;
    }

    public override string GetActionName()
    {
        return "Arrow Shoot";
    }

    protected override void Attack()
    {
        SetAttackStat();

        OnStartArrowShootAction?.Invoke(this, new OnStartArrowShootActionArgs {targetUnit = targetUnit, attackingUnit = unit});

        targetUnit.Damage(isMagical, damageType, attackValue, attackStat );
    }
}
