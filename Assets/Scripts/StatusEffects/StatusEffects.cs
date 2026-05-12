using System;
using UnityEngine;

public abstract class StatusEffects : MonoBehaviour
{
    protected Unit unit;
    protected int duration ;


    protected void Awake() 
    {
        unit = GetComponent<Unit>();
    }

    protected void Start() 
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        ApplyStatusEffects();
    }

    public abstract String GetStatusEffectName();

    public int GetStatusEffectsDuration()
    {
        return duration;
    }

    protected abstract void ApplyStatusEffects();
    
    protected void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        Unit testUnit = UnitActionSystem.Instance.GetSelectedUnit();
        if (testUnit == unit)
        {
            if (duration >= 0)
            {
                Destroy(gameObject);
            }
            duration--;
            DoEffectOnEveryTurn();
        }
    }

    protected void OnDestroy()
    {
        TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;

    }

    protected abstract void DoEffectOnEveryTurn();

    public void SetDuration(int duration)
    {
        this.duration = duration;
    }


}
