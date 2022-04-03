using Assets.Scripts.Base;

using UnityEngine;


public class PoliceTroopBehaviour : CoreUnitBehaviour
{
    protected static System.Collections.Generic.List<AudioClip> sendSounds = new System.Collections.Generic.List<AudioClip>()
    {
        GameFrame.Base.Resources.Manager.Audio.Get("Fanfare_1"),
        GameFrame.Base.Resources.Manager.Audio.Get("March_1"),
        GameFrame.Base.Resources.Manager.Audio.Get("March_2"),
        GameFrame.Base.Resources.Manager.Audio.Get("March_3"),
        GameFrame.Base.Resources.Manager.Audio.Get("March_4"),
        GameFrame.Base.Resources.Manager.Audio.Get("March_5"),
        GameFrame.Base.Resources.Manager.Audio.Get("Yes_Sir"),
        GameFrame.Base.Resources.Manager.Audio.Get("Yes_Sir_2")
    };

    protected static float sendSoundTick = 0;


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
        playSendSound();
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
            GameHandler.SelectTroop(null);
        }

        Core.Game.State.SecurityForces.Remove(this.PoliceTroop);

        GameObject.Destroy(gameObject);
    }

    protected void playSendSound()
    {
        int index = (int) Mathf.Floor(Random.Range(0, 7.99f));
        float duration = (float)sendSounds[index].samples / sendSounds[index].frequency;

        if(Time.time > sendSoundTick)
        {
            sendSoundTick = Time.time + duration;
            Core.Game.EffectsAudioManager.Play(sendSounds[index]);
        }
    }
}

