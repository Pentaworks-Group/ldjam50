using Assets.Scripts.Base;

using UnityEngine;


public class PoliceTroopBehaviour : CoreUnitBehaviour
{
    public PoliceTroop PoliceTroop
    {
        get
        {
            return this.GetUnit<PoliceTroop>();
        }
    }

    private new void Update()
    {
        if (Vector2.Distance(PoliceTroop.Location, PoliceTroop.Base.Location) < 0.08f)
        {
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
            if (distance < CoreUnit.Range)
            {
                rebel.Repel(distance, this);
            }
            if (distance < CoreUnit.Range)
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
            PoliceTroop.Health += PoliceTroop.Base.Healing * Time.deltaTime;
        }
    }

    protected override void KillUnit()
    {
        if (GameHandler.SelectedTroop == this)
        {
            GameHandler.SelectedTroop = null;
        }

        Core.Game.State.SecurityForces.Remove(this.PoliceTroop);

        GameObject.Destroy(gameObject);
    }
}

