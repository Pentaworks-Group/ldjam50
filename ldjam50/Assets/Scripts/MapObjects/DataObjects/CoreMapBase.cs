public class CoreMapBase : CoreUnit
{
    public float Healing { get; set; }
    public bool Destroyed { get; set; } = false;
    public bool GameOverOnDestruction { get; set; }
}
