using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaceDefault : BaseDefault
{
    public float SafeZoneRadius { get; set; } = 0.13f;

    public PalaceDefault()
    {
        Healing = 50;
        Health = 10;
        MaxHealth = 10;

        Pos_x = (1895f / 3840);
        Pos_y = 1f - (1043f / 2160);
        ObjectSize = 0.7f;
    }
}
