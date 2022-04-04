using System;

using GameFrame.Core.Extensions;

using Newtonsoft.Json;

public class CoreMapObject
{
    public String ImageName { get; set; }
    public GameFrame.Core.Math.Vector2 ActualLocation
    {
        get
        {
            return this.Location.ToFrame();
        }
        set
        {
            this.Location = value.ToUnity();
        }
    }
    public String Name { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }


    [JsonIgnore]
    public UnityEngine.Vector2 Location { get; set; }

}
