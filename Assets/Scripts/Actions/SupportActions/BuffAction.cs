using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BuffAction : SupportAction
{
    public event EventHandler<ActionArgsWithTwoUnits> OnBuffActionStarted;
    protected abstract StatTypes StatType {get;}

    protected abstract string StatusName {get;}

    protected abstract float Amount{get;}

    protected abstract int Duration{get;}


    public override string GetActionName()
    {
        return StatusName;
    }

    protected override void DoAction()
    {
        BuffDebuffEffects buffEffect = targetUnit.AddComponent<BuffDebuffEffects>();
        buffEffect.SetUpBuffDebuffEffects(StatType, Amount, Duration, StatusName);
        OnBuffActionStarted?.Invoke(this, new ActionArgsWithTwoUnits{targetUnit = targetUnit, activeUnit = unit});
    }


}
