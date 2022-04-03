using System.Collections.Generic;

public class GameHandler
{
    public static List<RebelBehaviour> Rebels { get; } = new List<RebelBehaviour>();
    public static PalaceBehaviour Palace { get; set; }

    public static PoliceTroopBehaviour SelectedTroop { get; set; }

    public static GameFieldSettings GameFieldSettings { get; set;  }


    public static void AddRebel(RebelBehaviour rebel)
    {
        Rebels.Add(rebel);
    }

    public static void RemoveRebel(RebelBehaviour rebel)
    {
        Rebels.Remove(rebel);
    }

    public static void Clear()
    {
        Rebels.Clear();
        Palace = null;
    }

    public static void Fight(CoreUnitBehaviour oppenent1, CoreUnitBehaviour opponent2, float distance)
    {
        float dmgScale = distance / 0.08f;
        oppenent1.DamageUnit(dmgScale * distance * opponent2.CoreUnit.Strength);
        opponent2.DamageUnit(dmgScale * distance * oppenent1.CoreUnit.Strength);
    }

    public static float SafeZoneRadius { get; } = 0.25f;

}
