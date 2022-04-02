using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUnit : CoreMapObject
{
    public float MaxSpeed { get; set; }
    public float Speed { get; set; }

    public Vector2 Target { get; set; }
    public CoreMapObject TargetObject { get; set; }

    public float Strength { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
}
