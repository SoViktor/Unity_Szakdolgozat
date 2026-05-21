using UnityEngine;

public class DarknessDoTAction : DoTAction
{
    protected override DamageTypes DoTDamageType => DamageTypes.Darkness;

    protected override string StatusName => "darkness DoT";

    protected override float Amount => 20;

    protected override int Duration => 6;

    protected override int Range => 4;

    protected override bool IsCirculiar => true;
}
