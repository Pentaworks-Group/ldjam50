public class PalaceDefault : BaseDefault
{
    public float SafeZoneRadius { get; set; } = 0.13f;

    public PalaceDefault()
    {
        Healing = 50;
        Health = 1e7f;
        MaxHealth = 1e7f;

        var posX = (1895f / 3840);
        var posY = 1f - (1043f / 2160);

        Position = new GameFrame.Core.Math.Vector2(posX, posY);

        Range = 0.07f;
        Repulsion = 1.0f;
        ObjectSize = 0.07f;
    }
}
