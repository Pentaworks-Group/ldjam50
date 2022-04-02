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

        for (int i = GameHandler.Rebels.Count - 1; i >= 0; i--)
        {
            RebelBehaviour rebel = GameHandler.Rebels[i];
            float distance = Vector2.Distance(rebel.MapObject.Location, PoliceTroop.Location);
            if (distance < 0.1f)
            {
                rebel.Repel(distance, this);
            }
            if (distance < 0.08f)
            {
                rebel.Fight(distance, this);
            }
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
        AddDistanceAction(0.008f, Stop);
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
        if (PoliceTroop.Health < PoliceTroop.MaxHealth)
        {
            PoliceTroop.Health += 5 * Time.deltaTime;
        }
    }

    protected override void KillUnit()
    {
        if (GameHandler.SelectedTroop == this)
        {
            GameHandler.SelectedTroop = null;
        }
        GameObject.Destroy(gameObject);
    }
}

