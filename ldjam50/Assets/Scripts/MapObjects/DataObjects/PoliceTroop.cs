using System;
using System.Collections.Generic;

public class PoliceTroop : CoreUnit
{
    public CoreMapBase Base { get; set; }
    public bool IsSelected { get; internal set; }
    public List<String> MarchSounds { get; set; }
}
