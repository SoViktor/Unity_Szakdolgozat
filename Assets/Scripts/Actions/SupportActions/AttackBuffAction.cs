using Unity.VisualScripting;
using UnityEngine;

public class AttackBuffAction : BuffAction
{
    protected override int range => 5;

    protected override bool isCirculiar => true;

    protected override StatTypes StatType => StatTypes.Attack;

    protected override string StatusName => "Attack Buff";

    protected override float Amount => 20;

    protected override int Duration => 2 ;

    public override string GetActionName()
    {
        return StatusName;
    }

    protected override void DoAction()
    {
        BuffDebuffEffects buffEffect = targetUnit.AddComponent<BuffDebuffEffects>();
        buffEffect.SetUpBuffDebuffEffects(StatType, Amount, Duration, StatusName);
    }
}
