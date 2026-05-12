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
    [SerializeField] private int baseMoveRange;

    private int healthPoints;
    private float defence;
    private float magicDefence;
    private float attack;
    private float magicAttack;
    private float dexterity;
    private int moveRange;

    private int baseActionValue;
    private int actionValue;
    private float baseRandom;


    private void Awake()
    {
        healthPoints = baseHealthPoints;
        defence = baseDefence;
        magicDefence = baseMagicDefence;
        attack = baseAttack;
        magicAttack = baseMagicAttack;
        dexterity = baseDexterity;
        moveRange = baseMoveRange;
        baseRandom = UnityEngine.Random.value;
        BaseActionValueSetUp();
        actionValue = baseActionValue;


    }

    public void Damage(bool isMagical, DamageTypes damageType, float damageValue, float attackStat)
    {
        TypeSystem typeSystem = new TypeSystem();
        float multiplier = typeSystem.GetMultiplier(damageType, unitType);
        damageValue *= multiplier;

        float random = UnityEngine.Random.Range(0.9f, 1.1f);
        damageValue *= random;

        Debug.Log(random);

        float critRandom = UnityEngine.Random.value;
        float critChance = 0.1f;
        float critDamage = 2f;
        if (critRandom <= critChance)
        {
            damageValue *= critDamage;
        }

        float damageDealt;

        if (isMagical)
        {
            damageDealt = attackStat / magicDefence * damageValue;
        } 
        else
        {
            damageDealt = attackStat / defence * damageValue;    
        }

        Debug.Log(attackStat);

        healthPoints -= Mathf.RoundToInt(damageDealt);

        if (healthPoints <= 0)
        {
            healthPoints = 0;
            Death();
        }
        Debug.Log( healthPoints);

    }

    public void Heal (int healAmount)
    {
        int newHealtPoints = healthPoints + healAmount;
        if (newHealtPoints > baseHealthPoints)
        {
            newHealtPoints = baseHealthPoints;
        }
        healthPoints = newHealtPoints;
    }

    private void Death()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public int GetBaseHealthPoints()
    {
        return baseHealthPoints;
    }

    public int GetHealthPoints()
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
    public int GetMoveRange()
    {
        return moveRange;
    }

    public float GetBaseRandom()
    {
        return baseRandom;
    }

    public int GetActionValue()
    {
        return actionValue;
    }
    public int GetBaseActionValue()
    {
        return baseActionValue;
    }

    public void BaseActionValueSetUp()
    {
        baseActionValue = Mathf.RoundToInt((100 / dexterity) * 100);
    }

    public void DecreaseActionValue(int amount)
    {
        actionValue -= amount;
    }

    public void ResetActionValue()
    {
        actionValue = baseActionValue;
    }

    public void ModifyAttack(float amount)
    {
        attack += amount;
    }

    public void ModifyMagicAttack(float amount)
    {
        magicAttack += amount;
    }

    public void ModifyDefence(float amount)
    {
        defence += amount;
    }

    public void ModifyMagicDefence(float amount)
    {
        magicDefence += amount;
    }


    public void ModifyMoveRange(int amount)
    {
        moveRange += amount;
    }
    public void ModifyDexterity(float amount)
    {
        int oldBaseActioValue = baseActionValue; 
        dexterity += amount;

        BaseActionValueSetUp();

        actionValue = Mathf.RoundToInt(actionValue * ((float)baseActionValue / oldBaseActioValue));
    }
}
