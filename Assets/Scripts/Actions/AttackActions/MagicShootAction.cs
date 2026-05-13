using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicShootAction : AttackAction
{
    protected override int Range => 7;

    protected override DamageTypes DamageType => DamageTypes.Blight;

    protected override bool IsMagical => true;

    protected override float AttackValue => 20f;

    protected override bool IsCirculiar => true;

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
