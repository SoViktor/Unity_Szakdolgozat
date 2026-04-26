using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicShootAction : AttackAction
{
    protected override int maxAttackDistance => 7;

    protected override DamageTypes damageType => DamageTypes.Blight;

    protected override bool isMagical => true;

    protected override float attackValue => 20f;

    protected override bool isAttackCirculiar => true;

    public event EventHandler<OnStartMagicShootActionArgs> OnStartMagicShootAction;

    public class OnStartMagicShootActionArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit attackingUnit;
    }
    
    protected override void Attack()
    {
        OnStartMagicShootAction?.Invoke(this, new OnStartMagicShootActionArgs{targetUnit =targetUnit, attackingUnit = unit});
        SetAttackStat();

        targetUnit.Damage(isMagical, damageType, attackValue, attackStat);
    }

    public override string GetActionName()
    {
        return "Magic Shoot";
    }


}
