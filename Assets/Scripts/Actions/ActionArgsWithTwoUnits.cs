using System;

public class ActionArgsWithTwoUnits : EventArgs
{
    public Unit ActiveUnit {get; set;}
    public Unit TargetUnit {get; set;}
}
