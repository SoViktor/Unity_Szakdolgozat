using System;
using UnityEngine;

public class BuffDebuffEffects : StatusEffects
{
    private float amount;
    private StatTypes statType;
    private String statusName;

    public BuffDebuffEffects(StatTypes statType, float amount, int duration, String statusName)
    {
        SetDuration(duration);
        this.statType = statType;
        this.amount = amount;
        this.statusName = statusName;
    }

    protected override void ApplyStatusEffects()
    {
        StatSystem statSystem = unit.GetStatSystem();
        switch (statType)
        {
            case StatTypes.Attack:
                statSystem.ModifyAttack(amount);
                break;

            case StatTypes.MagicAttack:
                statSystem.ModifyMagicAttack(amount);
                break;

            case StatTypes.Defence:
                statSystem.ModifyDefence(amount);
                break;

            case StatTypes.MagicDefence:
                statSystem.ModifyMagicDefence(amount);
                break;

            case StatTypes.Dexterity:
                statSystem.ModifyDexterity(amount);
                break;

            case StatTypes.MoveRange:
                statSystem.ModifyMoveRange(Mathf.RoundToInt(amount));
                break;
        }

    }

    protected override void DoEffectOnEveryTurn()
    {
        return;
    }

    public override string GetStatusEffectName()
    {
        return statusName;
    }
}
