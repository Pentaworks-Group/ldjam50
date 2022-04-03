using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldSettings
{
    public String Name { get; set; }


    public float FirstTick { get; set; }
    public float TickInterval { get; set; }
    public float TickIntervalFactor { get; set; }
    public float PalaceHealing { get; set; }



    public List<RebelDefault> RebelDefaults { get; set; }
    public List<TroopDefault> TroopDefaults { get; set; }

}
