using System;
using System.Collections.Generic;


public class BaseDefault : CoreMapObjectDefault
{
    public float Pos_x { get; set; }
    public float Pos_y { get; set; }
    public float Radius { get; set; }
    public float Healing { get; set; }
    public String Name { get; set; }
    public bool GameOverOnDestruction { get; set; }
}
