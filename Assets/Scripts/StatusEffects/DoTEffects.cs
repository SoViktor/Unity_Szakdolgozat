using System;
using UnityEngine;

public class DoTEffects : StatusEffects
{

    private DamageTypes damageType;
    private float amount;
    private float attackStat;
    private String statusName;

    public override string GetStatusEffectName()
    {
        return statusName;
    }
    protected override void ApplyStatusEffects()
    {
        return;
    }

    protected override void DoEffectOnEveryTurn()
    {
        StatSystem statSystem = unit.GetStatSystem();
        statSystem.DoTDamage(damageType, amount, attackStat );
    }

    protected override void EndStatusEffect()
    {
        return;
    }

    public void SetUpDoT(DamageTypes damageType, float amount,float bestAttackStat, int duration, String statusName)
    {
        SetDuration(duration);
        this.damageType = damageType;
        this.amount = amount;
        this.statusName = statusName;
        this.attackStat = bestAttackStat;

        TriggerStatusEffectAdded();
    }

    public DamageTypes GetDamageType()
    {
        return damageType;
    }


}
