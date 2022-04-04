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



    public PalaceDefault PalaceDefault { get; set; } = new PalaceDefault();

    public bool DisableMilitaryBase { get; set; }
    public PalaceDefault MilitaryBaseDefault { get; set; } = new PalaceDefault();

    public List<RebelDefault> RebelDefaults { get; set; }
    public List<TroopDefault> TroopDefaults { get; set; }
}
