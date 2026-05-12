using System;
using Unity.VisualScripting;
using UnityEngine;

public class DexterityBuff : BuffAction
{
    protected override StatTypes StatType => StatTypes.Dexterity;

    protected override string StatusName => "Dexteity Buff";

    protected override float Amount =>1000;

    protected override int Duration =>1;

    protected override int range => 5;

    protected override bool isCirculiar => true;


}
