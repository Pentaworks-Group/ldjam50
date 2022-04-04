using System;
using System.Collections.Generic;

public class GameFieldSettings
{
    public String Name { get; set; }

    public float FirstTick { get; set; }
    public float TickInterval { get; set; }
    public float TickIntervalFactor { get; set; } = 2;
    public float TickIntervalLogBase { get; set; } = 5;


    public bool DisableShop { get; set; }
    public Decimal MoneyGainPerInterval { get; set; }
    public float MoneyInterval { get; set; }
    public float MoneyFirstTick { get; set; }
    public Decimal MoneyStart { get; set; }

    public PalaceDefault PalaceDefault { get; set; } = new PalaceDefault()
    {
        Name = "Palace",
        ImageNames = new List<String>
        {
            "Palace"
        },
        Healing = 0,
        Health = 100,
        MaxHealth = 100,
        Range = 0.07f,
        Repulsion = 1.0f,
        ObjectSize = 0.07f,
        SafeZoneRadius = 0.13f,
        GameOverOnDestruction = true
    };

    public bool DisableMilitaryBase { get; set; }
    public PalaceDefault MilitaryBaseDefault { get; set; } = new PalaceDefault()
    {
        Name = "MilitaryBase",
        ImageNames = new List<String>
        {
            "MBase"
        },
        Healing = 50,
        Health = 100,
        MaxHealth = 100,
        Range = 0.04f,
        Repulsion = 1.0f,
        ObjectSize = 0.02f,
        SafeZoneRadius = 0.06f,
        Position = new GameFrame.Core.Math.Vector2(0.721f, 0.9175f)
    };

    public List<RebelDefault> RebelDefaults { get; set; }
    public List<TroopDefault> TroopDefaults { get; set; }
}
