using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSystem : MonoBehaviour
{
    public event EventHandler OnDeath;

    [SerializeField] private float baseHealthPoints;
    [SerializeField] private float baseDefence;
    [SerializeField] private float baseMagicDefence;
    [SerializeField] private float baseAttack;
    [SerializeField] private float baseMagicAttack;
    [SerializeField] private float baseDexterity;
    [SerializeField] private int baseMoveDistance;
    [SerializeField] private DamageTypes [] weeknes;
    [SerializeField] private DamageTypes [] resistens;

    private float healthPoints;
    private float defence;
    private float magicDefence;
    private float attack;
    private float magicAttack;
    private float dexterity;
    private int moveDistance;

    private void Start()
    {
        healthPoints = baseHealthPoints;
        defence = baseDefence;
        magicDefence = baseMagicDefence;
        attack = baseAttack;
        magicAttack = baseMagicAttack;
        dexterity = baseDexterity;
        moveDistance = baseMoveDistance;
    }

    public void Damage(bool isMagical, DamageTypes damageType, float damageValue, float attackStat)
    {
        float damageDelt;
        foreach (DamageTypes damageTypes in weeknes)
        {
            if (damageTypes == damageType)
            {
                damageValue *=2;
            }
        }

        foreach (DamageTypes damageTypes in resistens)
        {
            if (damageTypes == damageType)
            {
                damageValue /=2;
            }
        }

        if (isMagical)
        {
            damageDelt = attackStat / magicDefence * damageValue;
        } 
        else
        {
            damageDelt = attackStat / defence * damageValue;    
        }


        healthPoints -= damageDelt;

        if (healthPoints <= 0)
        {
            healthPoints = 0;
            Death();
        }
        Debug.Log( healthPoints);

    }

    private void Death()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public float GetHelthPoints()
    {
        return healthPoints;
    }
    public float GetDefence()
    {
        return defence;
    }
    public float GetMagicDefence()
    {
        return magicDefence;
    }
    public float GetAttack()
    {
        return attack;
    }
    public float GetMagicAttack()
    {
        return magicAttack;
    }
    public float GetDexterity()
    {
        return dexterity;
    }
    public int GetMoveDistance()
    {
        return moveDistance;
    }

}
