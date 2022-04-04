using System;
using System.Collections.Generic;

public class GameFieldSettings
{
    public String Name { get; set; }

    public float FirstTick { get; set; }
    public float TickInterval { get; set; }
    public float TickIntervalFactor { get; set; }
    public float MoneyInterval { get; set; }
    public float MoneyFirstTick { get; set; }
    public Decimal MoneyGainPerInterval { get; set; }
    public float PalaceHealing { get; set; }

    public bool DisableShop { get; set; }

    public List<RebelDefault> RebelDefaults { get; set; }
    public List<TroopDefault> TroopDefaults { get; set; }
}
