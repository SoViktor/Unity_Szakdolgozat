using UnityEngine;

public abstract class BuffAction : SupportAction
{
    protected abstract StatTypes StatType {get;}

    protected abstract string StatusName {get;}

    protected abstract float Amount{get;}

    protected abstract int Duration{get;}

    public override string GetActionName()
    {
        return StatusName;
    }

}
