using UnityEngine;

public class DefenceDebuffAction : DebuffAction
{
    protected override int Range => 1;

    protected override bool IsCirculiar => false;

    protected override StatTypes StatType => StatTypes.Defence;

    protected override string StatusName => "Defence Debuff";

    protected override float Amount => -20;

    protected override int Duration => 2 ;
}
