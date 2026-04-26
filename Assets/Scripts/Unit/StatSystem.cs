using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSystem : MonoBehaviour
{
    public event EventHandler OnDeath;

    [SerializeField] private DamageTypes unitType;
    [SerializeField] private int baseHealthPoints;
    [SerializeField] private float baseDefence;
    [SerializeField] private float baseMagicDefence;
    [SerializeField] private float baseAttack;
    [SerializeField] private float baseMagicAttack;
    [SerializeField] private float baseDexterity;
    [SerializeField] private int baseMoveDistance;

    private int healthPoints;
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
        TypeSystem typeSystem = new TypeSystem();
        float multiplier = typeSystem.GetMultiplier(damageType, unitType);
        damageValue *= multiplier;

        float random = UnityEngine.Random.Range(0.9f, 1.1f);
        damageValue *= random;

        Debug.Log(random);

        float damageDealt;

        if (isMagical)
        {
            damageDealt = attackStat / magicDefence * damageValue;
        } 
        else
        {
            damageDealt = attackStat / defence * damageValue;    
        }


        healthPoints -= Mathf.RoundToInt(damageDealt);

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

    public int GetHelthPoints()
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
