using UnityEngine;

public class DexterityDebuffAction : DebuffAction
{
    protected override StatTypes StatType => StatTypes.Dexterity;

    protected override string StatusName => "Dexterity Debuff";

    protected override float Amount =>-1000f;

    protected override int Duration =>1;

    protected override int Range => 5;

    protected override bool IsCirculiar => true;

}
