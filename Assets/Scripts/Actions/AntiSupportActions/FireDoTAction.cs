using UnityEngine;

public class FireDoTAction : DoTAction
{
    protected override DamageTypes DoTDamageType => DamageTypes.Fire;

    protected override string StatusName => "Burn";

    protected override float Amount => 30;

    protected override int Duration => 5;

    protected override int Range => 3;

    protected override bool IsCirculiar => true;
}
