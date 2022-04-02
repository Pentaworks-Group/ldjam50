using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler
{
    public static List<RebelBehaviour> Rebels { get; } = new List<RebelBehaviour>();
    public static PalaceBehaviour Palace;

    public static void AddRebel(RebelBehaviour rebel)
    {
        Rebels.Add(rebel);
        Debug.Log("Added rebel: " + Rebels);
    }

    public static void RemoveRebel(RebelBehaviour rebel)
    {
        Rebels.Remove(rebel);
        Debug.Log("Added rebel: " + Rebels);
    }

    public static void Clear()
    {
        Rebels.Clear();
        Palace = null;
    }
}
