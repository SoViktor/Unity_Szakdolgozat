using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicShootAction : AttackAction
{
    protected override int range => 7;

    protected override DamageTypes damageType => DamageTypes.Blight;

    protected override bool isMagical => true;

    protected override float attackValue => 20f;

    protected override bool isCirculiar => true;

    public event EventHandler<ActionArgsWithTwoUnits> OnStartMagicShootAction;
    
    public override string GetActionName()
    {
        return "Magic Shoot";
    }

    protected override void DoAction()
    {
        OnStartMagicShootAction?.Invoke(this, new ActionArgsWithTwoUnits{targetUnit =targetUnit, activeUnit = unit});
        Attack();

    }
}
