using System;
using System.Linq;

using Assets.Scripts.Base;

using GameFrame.Core.Extensions;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapObjectSpawner : MonoBehaviour
{
    public GameObject RebelTemplate;
    public GameObject PoliceTroopTemplate;
    public GameObject Map;

    public Text TimeDisplay;
    public PalaceBehaviour Palace;

    private float spawnInterval = 0f;
    private float moneyInterval = 0f;

    private float nextSpawnTick = 0f;
    private float nextMoneyTick = 0f;
    private float currentTime = 0;

    private float musicChangeTick = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (Core.Game.State == default)
        {
            Core.Game.ChangeScene(SceneNames.MainMenu);
            return;
        }

        this.spawnInterval = GameHandler.GameFieldSettings.TickInterval;
        this.moneyInterval = GameHandler.GameFieldSettings.MoneyInterval;

        GameHandler.Clear();

        Debug.Log("GameFieldSettings: " + GameHandler.GameFieldSettings.Name);
        InitPalace();

        if (Core.Game.State.SecurityForces?.Count > 0)
        {
            foreach (var securityForce in Core.Game.State.SecurityForces)
            {
                var spawnedTroop = SpawnTroop(securityForce);

                GameHandler.AddSecurityForce(spawnedTroop);

                if (securityForce.IsSelected)
                {
                    GameHandler.SelectTroop(spawnedTroop);
                }
            }

            if (GameHandler.SelectedTroop == null)
            {
                GameHandler.SelectTroop(GameHandler.SecurityForces.FirstOrDefault());
            }
        }
        else
        {
            var troop = SpawnTroop(default);

            GameHandler.AddSecurityForce(troop);
            GameHandler.SelectTroop(troop);
        }

        if (Core.Game.State.Rebels?.Count > 0)
        {
            foreach (var rebel in Core.Game.State.Rebels)
            {
                var spawnedRebel = SpawnRebel(rebel);

                GameHandler.AddRebel(spawnedRebel);
            }
        }

        currentTime = Core.Game.State.ElapsedTime;
        nextSpawnTick = Core.Game.State.NextRebelSpawn;
        nextMoneyTick = Core.Game.State.NextMoneySpawn;

        //        Core.Game.BackgroundAudioManager.Stop();
        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListGame1;
        //        Core.Game.BackgroundAudioManager.Resume();
    }


    // Update is called once per frame
    void Update()
    {
        if (Core.Game.State == default)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime > nextSpawnTick)
        {
            SpawnRebel();

            Core.Game.State.NextRebelSpawn = nextSpawnTick;
            spawnInterval = GameHandler.GameFieldSettings.TickIntervalFactor * GameHandler.GameFieldSettings.TickInterval / Mathf.Log(currentTime, GameHandler.GameFieldSettings.TickIntervalLogBase);
            nextSpawnTick = currentTime + spawnInterval;
            Debug.Log("Next Spawn: " + nextSpawnTick + "  interval: " + spawnInterval);
        }

        if (!GameHandler.GameFieldSettings.DisableShop)
        {
            if (currentTime > nextMoneyTick)
            {
                nextMoneyTick = currentTime + moneyInterval;

                Core.Game.State.NextMoneySpawn = nextMoneyTick;

                Core.Game.State.AvailableCredits += GameHandler.GameFieldSettings.MoneyGainPerInterval;
            }
        }

        if (currentTime > musicChangeTick)
        {
            Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListGame2;
        }

        UpdateTimeDisplay();
    }

    public void MoveSelectedTroop(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        float relPositionX;
        float relPositionY;
        if (Screen.width < Screen.height)
        {
            relPositionX = pointerData.position.y / Screen.height;
            relPositionY = 1 - (pointerData.position.x / Screen.width);
        }
        else
        {
            relPositionX = pointerData.position.x / Screen.width;
            relPositionY = pointerData.position.y / Screen.height;
        }

        GameHandler.SelectedTroop.SendTroopsToLocation(new Vector2(relPositionX, relPositionY));
    }

    private void UpdateTimeDisplay()
    {
        TimeDisplay.text = currentTime.ToString("F1");

        if (Core.Game.State != default)
        {
            Core.Game.State.ElapsedTime = currentTime;
        }
    }

    public void SpawnTroopFromDefault(TroopDefault securityForceDefault)
    {
        var troop = GetTroopFromDefault(securityForceDefault);

        var spawnedTroop = SpawnTroop(troop);

        GameHandler.AddSecurityForce(spawnedTroop);
        GameHandler.SelectTroop(spawnedTroop);
    }

    private PoliceTroopBehaviour SpawnTroop(PoliceTroop existingTroop = default)
    {
        var policeTroop = existingTroop;

        if (policeTroop == default)
        {
            policeTroop = GetTroopFromDefault(GameHandler.GameFieldSettings.TroopDefaults.GetRandomEntry());
        }
        else
        {
            policeTroop.Base = GameHandler.Palace.CoreMapBase; // this should be loaded correctly
        }

        GameObject troopOb = InstantiateGameObject(PoliceTroopTemplate, Map.transform);

        PoliceTroopBehaviour troopBehaviour = troopOb.GetComponent<PoliceTroopBehaviour>();

        troopBehaviour.gameObject.SetActive(true);
        troopBehaviour.Init(policeTroop);

        return troopBehaviour;
    }

    private GameObject InstantiateGameObject(GameObject template, Transform parent)
    {
        Quaternion rotation;

        if (Screen.width < Screen.height)
        {
            if (template.transform.rotation.z != template.transform.rotation.w)
            {
                rotation = Quaternion.identity;
            }
            else
            {
                rotation = new Quaternion(0, 0, 1f, 1f);
            }
        }
        else
        {
            rotation = Quaternion.identity;
        }

        return Instantiate(template, new Vector3(0, 0, 0), rotation, parent);
    }

    private PoliceTroop GetTroopFromDefault(TroopDefault troopDefault)
    {
        BaseDefault troopBase = troopDefault.Bases.GetRandomEntry();

        var policeTroop = new PoliceTroop()
        {
            Name = troopDefault.Names.GetRandomEntry(),
            Speed = 0,
            MaxSpeed = troopDefault.MaxSpeed,
            Strength = troopDefault.Strength,
            Repulsion = troopDefault.Repulsion,
            Health = troopDefault.Health,
            MaxHealth = troopDefault.MaxHealth,
            Location = new Vector2(troopBase.Pos_x, troopBase.Pos_y),
            ImageName = troopDefault.ImageName,
            Range = troopDefault.Range,
            Base = GameHandler.Palace.CoreMapBase,
            MarchSounds = troopDefault.MarchSounds,
            Color = troopDefault.Color.ToUnity()
        };

        Core.Game.State.SecurityForces.Add(policeTroop);

        return policeTroop;
    }

    private RebelBehaviour SpawnRebel(Rebel existingRebel = default)
    {
        var rebel = existingRebel;

        if (rebel == null)
        {
            //float speed = 0;
            RebelDefault rebelDefault = GameHandler.GameFieldSettings.RebelDefaults.GetRandomEntry();
            float speed = UnityEngine.Random.Range(rebelDefault.MinSpeed, rebelDefault.MaxSpeed);
            //Debug.Log("Speed: " + speed);
            rebel = new Rebel()
            {
                Name = GetRandomRebelName(),
                Speed = speed,
                Location = GetValidRandomLocation(),
                Target = GameHandler.Palace.MapObject.Location,
                ImageName = rebelDefault.ImageName,
                Strength = rebelDefault.Strength,
                Repulsion = rebelDefault.Repulsion,
                Range = rebelDefault.Range,
                Health = rebelDefault.Health,
                MaxHealth = rebelDefault.MaxHealth,
                KillSound = rebelDefault.KillSounds.GetRandomEntry(),
                SpawnSound = rebelDefault.SpawnSounds.GetRandomEntry()
            };

            Core.Game.State.Rebels.Add(rebel);
            Core.Game.AmbienceAudioManager.Resume();
            AudioClip spawnAudio = GameFrame.Base.Resources.Manager.Audio.Get(rebel.SpawnSound);
            Core.Game.EffectsAudioManager.Play(spawnAudio);
        }

        GameObject rebelOb = InstantiateGameObject(RebelTemplate, Map.transform);

        RebelBehaviour rebelBehaviour = rebelOb.GetComponent<RebelBehaviour>();

        rebelBehaviour.gameObject.SetActive(true);
        rebelBehaviour.Init(rebel);

        GameHandler.AddRebel(rebelBehaviour);

        return rebelBehaviour;
    }

    private String GetRandomRebelName()
    {
        return "Dope Rebel Name";
    }

    private Vector2 GetValidRandomLocation()
    {
        bool valid = false;
        Vector2 location = default;
        while (!valid)
        {
            float locationX = UnityEngine.Random.Range(0f, 1f);
            float locationY = UnityEngine.Random.Range(0f, 1f);
            location = new Vector2(locationX, locationY);

            float distance = GameHandler.GetDistance(location, GameHandler.Palace.MapObject.Location);
            if (distance > GameHandler.GameFieldSettings.SafeZoneRadius)
            {
                valid = true;
            }

        }

        return location;
    }

    private void InitPalace()
    {
        Palace.Healing = GameHandler.GameFieldSettings.PalaceHealing;
        GameHandler.Palace = Palace;
        GameHandler.Palace.InitPalace();
    }
}
