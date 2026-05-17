using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DebuffAction : AntiSupportAction
{
    public event EventHandler<ActionArgsWithTwoUnits> OnDebuffActionStarted;
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
        BuffDebuffEffects debuffEffect = targetUnit.AddComponent<BuffDebuffEffects>();
        debuffEffect.SetUpBuffDebuffEffects(StatType, Amount, Duration, StatusName);
        OnDebuffActionStarted?.Invoke(this, new ActionArgsWithTwoUnits{TargetUnit = targetUnit, ActiveUnit = unit});
    }

}
