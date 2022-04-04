
using Assets.Scripts.Base;

using GameFrame.Core.Extensions;

using UnityEngine;
using UnityEngine.UI;

public class SecurityForceBehaviour : CoreUnitBehaviour
{
    protected static float sendSoundTick = 0;

    private GameObject keyNumberArea;
    private Text keyNumberText;

    public SecurityForce SecurityForce
    {
        get
        {
            return this.GetUnit<SecurityForce>();
        }
    }

    public void SendTroopsToLocation(Vector2 target)
    {
        if (IsMoveable())
        {
            playSendSound();
            SecurityForce.Speed = SecurityForce.MaxSpeed;
            SecurityForce.Target = target;

            if (CoreUnit.MoveJustOnce && SecurityForce.IsMoveable)
            {
                SecurityForce.IsMoveable = false;
            }
        }
    }

    public void Init(SecurityForce policeTroop)
    {
        sizeScale = 0.2f;
        AddDistanceAction(0.008f, Stop);
        base.Init(policeTroop);
    }

    public void Stop(float distance)
    {
        SecurityForce.Speed = 0;
    }

    public void TroopClick()
    {
        if (GameHandler.SelectedTroop != this && IsMoveable())
        {
            GameHandler.SelectTroop(this);
        }
    }

    protected override void KillObject()
    {
        if (GameHandler.SelectedTroop == this)
        {
            GameHandler.SelectTroop(null);
        }

        GameHandler.RemoveSecurityForce(this);

        try
        {
            Destroy(gameObject);
        }
        catch { }
    }

    public override bool IsMoveable()
    {
        if (SecurityForce != default)
        {
            return SecurityForce.IsMoveable;
        }

        return false;
    }

    protected void playSendSound()
    {
        AudioClip audioClip = GameFrame.Base.Resources.Manager.Audio.Get(SecurityForce.MarchSounds.GetRandomEntry());
        float duration = (float)audioClip.samples / audioClip.frequency;

        if (Time.time > sendSoundTick)
        {
            sendSoundTick = Time.time + duration;
            Core.Game.EffectsAudioManager.Play(audioClip);
        }
    }

    private void Start()
    {
        this.keyNumberArea = this.gameObject.transform.Find("Active/KeyNumberArea").gameObject;
        this.keyNumberText = this.keyNumberArea.transform.Find("KeyNumberText").GetComponent<Text>();
    }

    private new void Update()
    {
        if (Vector2.Distance(SecurityForce.Location, SecurityForce.Base.Location) < 0.08f)
        {
            Heal();
        }

        if (this.SecurityForce.IsSelected)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SendTroopsToLocation(SecurityForce.Base.Location);
            }

            CheckImageColor(this.BackgroundImage, this.SecurityForce.SelectedColor.ToUnity());
        }
        else
        {
            CheckImageColor(this.BackgroundImage, this.SecurityForce.BackgroundColor.ToUnity());
        }

        CheckForAdjesentRebels();

        if (this.SecurityForce.AssignedKey.HasValue)
        {
            if (!keyNumberArea.activeSelf)
            {
                keyNumberArea.SetActive(true);
            }

            this.keyNumberText.text = this.SecurityForce.AssignedKey.Value.ToString();
        }

        if (keyNumberArea.activeSelf && !this.SecurityForce.AssignedKey.HasValue)
        {
            keyNumberArea.SetActive(false);
        }

        if (!IsMoveable())
        {
            BackgroundImage.gameObject.SetActive(false);
        }

        base.Update();
    }

    private void CheckImageColor(Image image, Color colorToCheck)
    {
        if (image?.color != colorToCheck)
        {
            image.color = colorToCheck;
        }
    }

    private void CheckForAdjesentRebels()
    {
        for (int i = GameHandler.Rebels.Count - 1; i >= 0; i--)
        {
            RebelBehaviour rebel = GameHandler.Rebels[i];

            float distance = GameHandler.GetDistance(rebel.MapObject.Location, SecurityForce.Location);

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
        if (SecurityForce.Health < SecurityForce.MaxHealth)
        {
            SecurityForce.Health += SecurityForce.Base.Healing * Time.deltaTime;
        }
    }
}

