using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DoTAction : AntiSupportAction
{
    public event EventHandler<ActionArgsWithTwoUnits> OnDoTActionStarted;
    protected abstract DamageTypes DoTDamageType {get;}

    protected abstract string StatusName {get;}

    protected abstract float Amount{get;}

    protected abstract int Duration{get;}

    protected float bestAttack;

    public override string GetActionName()
    {
        return StatusName;
    }

    protected override void DoAction()
    {
        DoTEffects doTEffect = targetUnit.AddComponent<DoTEffects>();
        SetBestAttack();
        doTEffect.SetUpDoT(DoTDamageType, Amount, bestAttack, Duration, StatusName);
        OnDoTActionStarted?.Invoke(this, new ActionArgsWithTwoUnits{targetUnit = targetUnit, activeUnit = unit});
    }

    protected void SetBestAttack()
    {
        StatSystem statSystem = unit.GetStatSystem();
        bestAttack=statSystem.GetBestAttack();
    }
}
