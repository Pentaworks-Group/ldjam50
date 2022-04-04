using System;
using System.Collections.Generic;

using Assets.Scripts.Base;
using UnityEngine;

public class GameHandler
{
    public static List<SecurityForceBehaviour> SecurityForces { get; } = new List<SecurityForceBehaviour>();
    public static List<RebelBehaviour> Rebels { get; } = new List<RebelBehaviour>();
    public static PalaceBehaviour Palace { get; set; }
    public static PalaceBehaviour MilitaryBase { get; set; }

    public static SecurityForceBehaviour SelectedTroop { get; private set; }

    public static GameFieldSettings GameFieldSettings { get; set; }
    public static List<GameFieldSettings> AvailableGameModes { get; set; }

    public static void AddRebel(RebelBehaviour rebel)
    {
        Rebels.Add(rebel);
    }

    public static void RemoveRebel(RebelBehaviour rebel)
    {
        Core.Game.State.Rebels.Remove(rebel.Rebel);
        Rebels.Remove(rebel);
    }

    public static void AddSecurityForce(SecurityForceBehaviour policeTroopBehaviour)
    {
        SecurityForces.Add(policeTroopBehaviour);
    }

    public static void RemoveSecurityForce(SecurityForceBehaviour policeTroopBehaviour)
    {
        Core.Game.State.SecurityForces.Remove(policeTroopBehaviour.SecurityForce);
        SecurityForces.Remove(policeTroopBehaviour);
    }

    public static void SelectTroop(SecurityForceBehaviour policeTroopBehaviour)
    {
        if (SelectedTroop != null)
        {
            SelectedTroop.SecurityForce.IsSelected = false;
        }

        SelectedTroop = policeTroopBehaviour;

        if (policeTroopBehaviour?.SecurityForce != default)
        {
            policeTroopBehaviour.SecurityForce.IsSelected = true;
        }
    }

    public static void Clear()
    {
        Rebels.Clear();
        SecurityForces.Clear();
        SelectedTroop = null;
        Palace = null;
    }

    public static void Fight(CoreMapObjectBehaviour opponent1, CoreMapObjectBehaviour opponent2, float distance)
    {
        //Reduce Distance by Object sizes
        float d = distance - opponent1.MapObject.ObjectSize - opponent2.MapObject.ObjectSize;
        if (d < opponent2.MapObject.Range)
        {
            opponent1.DamageObject((1 - d / opponent2.MapObject.Range) * opponent2.MapObject.Strength);
        }
        if (d < opponent1.MapObject.Range)
        {
            opponent2.DamageObject((1 - d / opponent1.MapObject.Range) * opponent1.MapObject.Strength);
        }
    }

    public static void Repel(CoreMapObjectBehaviour opponent1, CoreMapObjectBehaviour opponent2, float distance)
    {
        Vector2 direction = opponent1.MapObject.Location - opponent2.MapObject.Location;
        direction.Normalize();
        if (distance < opponent2.MapObject.Range)        {
            float repulsionStrength = (1 - distance / opponent2.MapObject.Range) * opponent2.MapObject.Repulsion;
            Vector2 repulsion = new Vector2(direction.x, direction.y) * repulsionStrength;
            if (opponent1.IsMoveable())
                opponent1.MoveInDirection(repulsion);
        }
        if (distance < opponent1.MapObject.Range)
        {
            float repulsionStrength = (1 - distance / opponent1.MapObject.Range) * opponent1.MapObject.Repulsion;
            Vector2 repulsion = direction * repulsionStrength * -1;
            if (opponent2.IsMoveable())
                opponent2.MoveInDirection(repulsion);
        }
    }

    public static float GetDistance(Vector2 location1, Vector2 location2)
    {
        Vector2 loc1 = new Vector2(location1.x * Screen.width, location1.y * Screen.height);
        Vector2 loc2 = new Vector2(location2.x * Screen.width, location2.y * Screen.height);
        float distance = Vector2.Distance(loc1, loc2) / Mathf.Sqrt(Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2));
        return distance;
    }
}
