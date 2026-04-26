using System.Collections.Generic;

public class TypeSystem
{
    private static readonly Dictionary<(DamageTypes attacker, DamageTypes defender), float> damageTable =
        new Dictionary<(DamageTypes attacker, DamageTypes defender), float>()
    {
    
        { (DamageTypes.Lightning, DamageTypes.Steel), 2f },
        { (DamageTypes.Water, DamageTypes.Steel), 2f },
        { (DamageTypes.Ice, DamageTypes.Steel), 0.5f },
        { (DamageTypes.Blight, DamageTypes.Steel), 0.5f },

        { (DamageTypes.Fire, DamageTypes.Plant), 2f },
        { (DamageTypes.Ice, DamageTypes.Plant), 2f },
        { (DamageTypes.Earth, DamageTypes.Plant), 2f },
        { (DamageTypes.Water, DamageTypes.Plant), 0.5f },
        { (DamageTypes.Blight, DamageTypes.Plant), 0.5f },

        { (DamageTypes.Water, DamageTypes.Fire), 2f },
        { (DamageTypes.Plant, DamageTypes.Fire), 0.5f },
        { (DamageTypes.Ice, DamageTypes.Fire), 0.5f },

        { (DamageTypes.Fire, DamageTypes.Ice), 2f },
        { (DamageTypes.Steel, DamageTypes.Ice), 2f },
        { (DamageTypes.Earth, DamageTypes.Ice), 0.5f },
        { (DamageTypes.Plant, DamageTypes.Ice), 0.5f },

        { (DamageTypes.Darkness, DamageTypes.Blight), 2f },
        { (DamageTypes.Steel, DamageTypes.Blight), 2f },
        { (DamageTypes.Plant, DamageTypes.Blight), 2f },
        { (DamageTypes.Light, DamageTypes.Blight), 0.5f },

        { (DamageTypes.Blight, DamageTypes.Light), 2f },
        { (DamageTypes.Darkness, DamageTypes.Light), 2f },
        { (DamageTypes.Light, DamageTypes.Light), 0.5f },

        { (DamageTypes.Earth, DamageTypes.Lightning), 2f },
        { (DamageTypes.Steel, DamageTypes.Lightning), 0.5f },
        { (DamageTypes.Water, DamageTypes.Lightning), 0.5f },

        { (DamageTypes.Plant, DamageTypes.Water), 2f },
        { (DamageTypes.Lightning, DamageTypes.Water), 2f },
        { (DamageTypes.Fire, DamageTypes.Water), 0.5f },
        { (DamageTypes.Steel, DamageTypes.Water), 0.5f },

        { (DamageTypes.Ice, DamageTypes.Earth), 2f },
        { (DamageTypes.Lightning, DamageTypes.Earth), 0.5f },
        { (DamageTypes.Plant, DamageTypes.Earth), 0.5f },

        { (DamageTypes.Light, DamageTypes.Darkness), 2f },
        { (DamageTypes.Blight, DamageTypes.Darkness), 0.5f },
    };

    public float GetMultiplier(DamageTypes attackerType, DamageTypes defenderType)
    {
        if (damageTable.TryGetValue((attackerType, defenderType), out float multiplier))
        {
            return multiplier;
        }

        return 1f;
    }
}
