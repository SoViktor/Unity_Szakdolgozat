using Unity.VisualScripting;
using UnityEngine;

public class AttackBuffAction : SupportAction
{
    protected override int range => 5;

    protected override bool isCirculiar => true;

    private StatTypes statType = StatTypes.Attack;

    private string statusName = "Attack Buff";

    private float amount = 20;

    private int duration = 3;

    public override string GetActionName()
    {
        return statusName;
    }

    protected override void DoAction()
    {
        BuffDebuffEffects buffEffect = new BuffDebuffEffects(statType, amount, duration, statusName);

    }
}
