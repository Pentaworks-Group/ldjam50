using System;
using System.Collections.Generic;

using GameFrame.Core.Media;

public class TroopDefault : CoreMapObjectDefault
{
    public String Type { get; set; }
    public List<String> Names { get; set; }
    public float MaxSpeed { get; set; }
    public float Strength { get; set; }
    public Decimal UnitCost { get; set; }
    public List<String> MarchSounds { get; set; }
    public List<BaseDefault> Bases { get; set; }
    public Color Color { get; set; }
    public Color SelectedColor { get; set; }
    public Boolean MoveJustOnce { get; set; } = false;
}
