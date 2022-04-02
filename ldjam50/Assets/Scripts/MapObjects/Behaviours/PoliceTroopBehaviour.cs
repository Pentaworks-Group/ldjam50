using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Base;


public class PoliceTroopBehaviour : CoreUnitBehaviour
{
    private PoliceTroop PoliceTroop;

    private new void Update()
    {
        if (Vector2.Distance(PoliceTroop.Location, PoliceTroop.Base.Location) < 0.08f) {
            Heal();
        }
        CheckForAdjesentRebels();
        base.Update();
    }

    private void CheckForAdjesentRebels()
    {
        foreach (RebelBehaviour rebel in GameHandler.Rebels)
        {
            float distance = Vector2.Distance(rebel.MapObject.Location, PoliceTroop.Location);
            if (distance < 0.1f)
            {
                rebel.Repel(distance, PoliceTroop);
            }
            //if (distance < 0.08f)
            //{
            //    rebel.Fight(distance, PoliceTroop);
            //}
        }
    }

    public void SendTroopsToLocation(Vector2 target)
    {
        PoliceTroop.Speed = PoliceTroop.MaxSpeed;
        PoliceTroop.Target = target;
       
    }

    public void Init(PoliceTroop policeTroop)
    {
        PoliceTroop = policeTroop;
        sizeScale = 0.2f;
        AddDistanceAction(0.001f, Stop);
        base.Init(policeTroop);
    }

    public void Stop(float distance)
    {
        PoliceTroop.Speed = 0;
    }

    public void TroopClick()
    {
        Debug.Log("Selected");
    }

    private void Heal()
    {
        if (PoliceTroop.Strength < PoliceTroop.MaxStrength)
        {
            PoliceTroop.Strength += 5 * Time.deltaTime;
        }
    }
}

