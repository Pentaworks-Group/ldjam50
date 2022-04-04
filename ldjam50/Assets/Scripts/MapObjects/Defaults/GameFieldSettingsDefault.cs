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



    public PalaceDefault PalaceDefault { get; set; } = new PalaceDefault()
    {
        Name = "Palace",
        ImageName = "Palace",
        Healing = 0,
        Health = 100,
        MaxHealth = 100,
        Range = 0.07f,
        Repulsion = 1.0f,
        ObjectSize = 0.04f,
        SafeZoneRadius = 0.13f
    };

    public bool DisableMilitaryBase { get; set; }
    public PalaceDefault MilitaryBaseDefault { get; set; } = new PalaceDefault()
    {
        Name = "MilitaryBase",
        ImageName = "MBase",
        Healing = 0,
        Health = 100,
        MaxHealth = 100,
        Range = 0.07f,
        Repulsion = 1.0f,
        ObjectSize = 0.04f,
        Pos_x = 0.721f,
        Pos_y = 0.9175f

    };

    public List<RebelDefault> RebelDefaults { get; set; }
    public List<TroopDefault> TroopDefaults { get; set; }
}
