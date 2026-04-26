using System;
using System.Collections.Generic;
using UnityEngine;

public class SlashAction : AttackAction
{
    protected override int maxAttackDistance => 1;

    protected override DamageTypes damageType => DamageTypes.Slasing;

    protected override bool isMagical => false;

    protected override float attackValue => 20f;

    protected override bool isAttackCirculiar => false;

    public event EventHandler OnStartSlashAction;

    protected override void Attack()
    {
        OnStartSlashAction?.Invoke(this, EventArgs.Empty);
        
        targetUnit.Damage(isMagical, damageType, attackValue, attackStat);
    }

    public override string GetActionName()
    {
        return "Slash";
    }



}
