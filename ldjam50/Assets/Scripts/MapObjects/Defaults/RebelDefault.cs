using System;
using System.Collections.Generic;

public class RebelDefault : CoreMapObjectDefault
{
    public String Type { get; set; }
    public List<String> Names { get; set; }
    public float MinSpeed { get; set; }
    public float MaxSpeed { get; set; }
    public float Strength { get; set; }
    public float Repulsion { get; set; }
    public float Range { get; set; }
}
