using System;
using System.Collections.Generic;

using GameFrame.Core.Math;

public class BaseDefault : CoreMapObjectDefault
{
    public Vector2 Position { get; set; }
    public float Radius { get; set; }
    public float Healing { get; set; }
    public String Name { get; set; }
    public bool GameOverOnDestruction { get; set; }
}
