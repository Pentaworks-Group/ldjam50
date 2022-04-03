using System;

using Assets.Scripts.Base;
using GameFrame.Core.Extensions;

using UnityEngine;
using UnityEngine.UI;

public class PoliceTroopBehaviour : CoreUnitBehaviour
{
    protected static Lazy<System.Collections.Generic.List<AudioClip>> lazySendSounds = new Lazy<System.Collections.Generic.List<AudioClip>>(() =>
    {
        return new System.Collections.Generic.List<AudioClip>()
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
    });

    protected static float sendSoundTick = 0;

    private static Color selectedColor = new Color(0,0,0,125);
    private static Color defaultColor = new Color(0,0,0,125);

    private Image image;

    public PoliceTroop PoliceTroop
    {
        get
        {
            return this.GetUnit<PoliceTroop>();
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
        if (GameHandler.SelectedTroop != this)
        {
            GameHandler.SelectTroop(this);
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
        AudioClip audioClip = lazySendSounds.Value.GetRandomEntry();
        float duration = (float)audioClip.samples / audioClip.frequency;

        if (Time.time > sendSoundTick)
        {
            sendSoundTick = Time.time + duration;
            Core.Game.EffectsAudioManager.Play(audioClip);
        }
    }

    private void Start()
    {
        this.image = GetComponent<Image>();

        defaultColor = this.image.color;
    }

    private new void Update()
    {
        if (Vector2.Distance(PoliceTroop.Location, PoliceTroop.Base.Location) < 0.08f)
        {
            Heal();
        }

        if (this.PoliceTroop.IsSelected)
        {
            this.image.color = selectedColor;
        }
        else if (this.image.color != null)
        {
            this.image.color = defaultColor;
        }

        CheckForAdjesentRebels();

        base.Update();
    }

    private void CheckForAdjesentRebels()
    {
        for (int i = GameHandler.Rebels.Count - 1; i >= 0; i--)
        {
            RebelBehaviour rebel = GameHandler.Rebels[i];

            float distance = GameHandler.GetDistance(rebel.MapObject.Location, PoliceTroop.Location);

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

    private void Heal()
    {
        if (PoliceTroop.Health < PoliceTroop.MaxHealth)
        {
            PoliceTroop.Health += PoliceTroop.Base.Healing * Time.deltaTime;
        }
    }
}

