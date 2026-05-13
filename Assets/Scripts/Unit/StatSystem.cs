using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSystem : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds0_05 = new(0.05f);

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
        TypeSystem typeSystem = new();
        float multiplier = typeSystem.GetMultiplier(damageType, unitType);
        damageValue *= multiplier;

        float random = UnityEngine.Random.Range(0.9f, 1.1f);
        damageValue *= random;

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

        healthPoints -= Mathf.RoundToInt(damageDealt);

        if (healthPoints <= 0)
        {
            healthPoints = 0;
            Death();
        }
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

    public void DoTDamage(DamageTypes damageType, float damageValue, float attackStat)
    {
        TypeSystem typeSystem = new();
        float multiplier = typeSystem.GetMultiplier(damageType, unitType);
        damageValue *= multiplier;

        int damageDealt = Mathf.RoundToInt( attackStat / Mathf.Max(magicDefence, defence) * damageValue);



        if (damageDealt >= healthPoints)
        {
            StartCoroutine(HandelDotDeath());
        }
        else
        {
            healthPoints -= damageDealt;
        }
    }

    private IEnumerator HandelDotDeath()
    {
        TurnSystem.Instance.Nextturn();

        yield return _waitForSeconds0_05;

        Death();

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
        if ((attack + amount) > 1)
        {
            attack += amount;
        }
        else
        {
            attack = 1;
            Debug.Log("Unnormal stat change");
        }
    }

    public void ModifyMagicAttack(float amount)
    {
        if ((magicAttack + amount) > 1)
        {
            magicAttack += amount;
        }
        else
        {
            magicAttack = 1;
            Debug.Log("Unnormal stat change");

        }
    }

    public void ModifyDefence(float amount)
    {
        if ((defence + amount) > 1)
        {
            defence += amount;
        }
        else
        {
            defence = 1;
            Debug.Log("Unnormal stat change");
        }
    }

    public void ModifyMagicDefence(float amount)
    {
        if ((magicDefence + amount) > 1)
        {
            magicDefence += amount;
        }
        else
        {
            magicDefence = 1;
            Debug.Log("Unnormal stat change");
        }
    }


    public void ModifyMoveRange(int amount)
    {
        if ((moveRange + amount) > 1)
        {
            moveRange += amount;
        }
        else
        {
            moveRange = 1;
            Debug.Log("Unnormal stat change");
        }
    }
    public void ModifyDexterity(float amount)
    {
        int oldBaseActioValue = baseActionValue; 
        if ((dexterity + amount) > 10)
        {
            dexterity += amount;
        }
        else
        {
            dexterity = 10;
            Debug.Log("Unnormal stat change");
        }

        BaseActionValueSetUp();

        actionValue = Mathf.RoundToInt(actionValue * ((float)baseActionValue / oldBaseActioValue));
    }

    public float GetBestAttack()
    {
        return Mathf.Max(attack, magicAttack);
    }
}
