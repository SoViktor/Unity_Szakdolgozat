using UnityEngine;

public class AttackDebuffAction : DebuffAction
{
    protected override int Range => 5;

    protected override bool IsCirculiar => true;

    protected override StatTypes StatType => StatTypes.Attack;

    protected override string StatusName => "Attack Debuff";

    protected override float Amount => -20;

    protected override int Duration => 2 ;

}
