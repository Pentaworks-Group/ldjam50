using System;

using Assets.Scripts.Base;

using GameFrame.Core.Extensions;

using UnityEngine;
using UnityEngine.UI;

public class PoliceTroopBehaviour : CoreUnitBehaviour
{
    protected static float sendSoundTick = 0;

    private static Color selectedColor = Color.white;
    private static Color defaultColor = new Color(0.5f, 0.5f, 0.5f, 1f);

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
        AudioClip audioClip = GameFrame.Base.Resources.Manager.Audio.Get(PoliceTroop.MarchSounds.GetRandomEntry());
        float duration = (float)audioClip.samples / audioClip.frequency;

        if (Time.time > sendSoundTick)
        {
            sendSoundTick = Time.time + duration;
            Core.Game.EffectsAudioManager.Play(audioClip);
        }
    }

    private void Start()
    {
    }

    private new void Update()
    {
        if (Vector2.Distance(PoliceTroop.Location, PoliceTroop.Base.Location) < 0.08f)
        {
            Heal();
        }

        if (this.PoliceTroop.IsSelected)
        {
            CheckImageColor(selectedColor);
        }
        else
        {
            CheckImageColor(defaultColor);
        }

        CheckForAdjesentRebels();

        base.Update();
    }

    private void CheckImageColor(Color colorToCheck)
    {
        if (this.Image?.color != colorToCheck)
        {
            this.Image.color = colorToCheck;
        }
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

