using System;
using Unity.VisualScripting;
using UnityEngine;

public class DexterityBuffAction : BuffAction
{
    protected override StatTypes StatType => StatTypes.Dexterity;

    protected override string StatusName => "Dexteity Buff";

    protected override float Amount =>20;

    protected override int Duration =>1;

    protected override int Range => 5;

    protected override bool IsCirculiar => true;


}
