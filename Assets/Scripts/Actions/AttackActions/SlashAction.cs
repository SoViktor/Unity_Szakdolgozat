using System;
using System.Collections.Generic;
using UnityEngine;

public class SlashAction : AttackAction
{
    protected override int range => 1;

    protected override DamageTypes damageType => DamageTypes.Steel;

    protected override bool isMagical => false;

    protected override float attackValue => 20f;

    protected override bool isCirculiar => false;

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
