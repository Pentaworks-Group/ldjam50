
using GameFrame.Core.Extensions;

using Newtonsoft.Json;

public class CoreUnit : CoreMapObject
{
    public float MaxSpeed { get; set; }
    public float Speed { get; set; }
    public GameFrame.Core.Math.Vector2 ActualTarget
    {
        get
        {
            return this.Target.ToFrame();
        }
        set
        {
            this.Target = value.ToUnity();
        }
    }

    public CoreMapObject TargetObject { get; set; }

    public float Strength { get; set; }
    public float Health { get; set; }
    public float Range { get; set; }
    public float MaxHealth { get; set; }


    [JsonIgnore]
    public UnityEngine.Vector2 Target { get; set; }
    
}
