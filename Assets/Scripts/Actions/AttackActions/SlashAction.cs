using System;
using System.Collections.Generic;
using UnityEngine;

public class SlashAction : AttackAction
{
    protected override int Range => 1;

    protected override DamageTypes DamageType => DamageTypes.Steel;

    protected override bool IsMagical => false;

    protected override float AttackValue => 20f;

    protected override bool IsCirculiar => false;

    public event EventHandler OnStartSlashAction;

    public override string GetActionName()
    {
        return "Slash";
    }

    protected override void DoAction()
    {
        
        OnStartSlashAction?.Invoke(this, EventArgs.Empty);
        Attack();
    }
}
