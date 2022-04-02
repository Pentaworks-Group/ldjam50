using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUnit : CoreMapObject
{
    public float MaxSpeed { get; set; }
    public float Speed { get; set; }

    public float Growth { get; set; }
    public Vector2 Target { get; set; }
}
