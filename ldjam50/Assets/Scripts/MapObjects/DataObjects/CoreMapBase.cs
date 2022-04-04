using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMapBase : CoreUnit
{
    public float Healing { get; set; }
    public bool Destroyed { get; set; } = false;
    public bool GameOverOnDestruction { get; set; }


}
