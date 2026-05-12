using System;
using UnityEngine;

public abstract class StatusEffects : MonoBehaviour
{
    public static event EventHandler<EventArgsWithOneUnit> OnAnyStatusEffectApplied;
    public static event EventHandler<EventArgsWithOneUnit> OnAnyStatusEffectRemoved;

    protected Unit unit;
    protected int duration ;


    protected void Awake() 
    {
        unit = GetComponent<Unit>();
    }

    protected void Start() 
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
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
            if (duration <= 0)
            {
                Debug.Log("duration <=0");
                EndStatusEffect();
                Destroy(this);
            }
            duration--;
            DoEffectOnEveryTurn();
            Debug.Log(unit.GetStatSystem().GetAttack());
        }
    }

    protected void OnDestroy()
    {
        Debug.Log("StatusEffect Destroyed");
        OnAnyStatusEffectRemoved?.Invoke(this, new EventArgsWithOneUnit{unit = unit});
        TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;

    }

    protected abstract void DoEffectOnEveryTurn();

    public void SetDuration(int duration)
    {
        this.duration = duration;
    }

    protected abstract void EndStatusEffect();

    protected void TriggerStatusEffectAdded()
    {
        OnAnyStatusEffectApplied?.Invoke(this, new EventArgsWithOneUnit{unit = unit,});
    }

}
