using System;
using System.Collections.Generic;

using UnityEngine;

public class PoliceTroop : CoreUnit
{
    public CoreMapBase Base { get; set; }
    public bool IsSelected { get; internal set; }
    public Color Color { get; set; }
    public List<String> MarchSounds { get; set; }
}
