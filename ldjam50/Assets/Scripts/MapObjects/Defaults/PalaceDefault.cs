using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaceDefault : BaseDefault
{
    public float SafeZoneRadius { get; set; } = 0.13f;

    public PalaceDefault()
    {
        Healing = 50;
        Health = 1e7f;
        MaxHealth = 1e7f;

        Pos_x = (1895f / 3840);
        Pos_y = 1f - (1043f / 2160);
        Range = 0.07f;
        Repulsion = 1.0f;
        ObjectSize = 0.07f;
    }
}
