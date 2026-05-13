using UnityEngine;

public class LightningDoTAction : DoTAction
{
    protected override DamageTypes DoTDamageType => DamageTypes.Lightning;

    protected override string StatusName => "Lightning DoT";

    protected override float Amount => 10;

    protected override int Duration => 10;

    protected override int Range => 7;

    protected override bool IsCirculiar => false;
}
