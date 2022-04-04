using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMapObjectDefault
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Repulsion { get; set; }
    public float Range { get; set; }
    public float ObjectSize { get; set; }
    public String ImageName { get; set; }
    public List<String> KillSounds { get; set; }
    public List<String> SpawnSounds { get; set; }
}
