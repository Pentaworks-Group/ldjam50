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
    public PalaceBehaviour MilitaryBase;

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

        var gameState = Core.Game.State;

        this.spawnInterval = gameState.Mode.TickInterval;
        this.moneyInterval = gameState.Mode.MoneyInterval;

        GameHandler.Clear();

        Debug.Log("GameFieldSettings: " + gameState.Mode.Name);
        InitPalace();
        InitMilitaryBase();

        if (gameState.SecurityForces?.Count > 0)
        {
            foreach (var securityForce in gameState.SecurityForces)
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
            if(Core.Game.State.Mode.MoneyStart==0) 
                SpawnTroopFromDefault(gameState.Mode.TroopDefaults.FirstOrDefault());
        }

        if (gameState.Rebels?.Count > 0)
        {
            foreach (var rebel in gameState.Rebels)
            {
                var spawnedRebel = SpawnRebel(rebel);

                GameHandler.AddRebel(spawnedRebel);
            }
        }

        currentTime = gameState.ElapsedTime;
        nextSpawnTick = gameState.NextRebelSpawn;
        nextMoneyTick = gameState.NextMoneySpawn;

        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListGame1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Core.Game.State == default)
        {
            return;
        }

        var gameState = Core.Game.State;

        currentTime += Time.deltaTime;

        if (currentTime > nextSpawnTick)
        {
            SpawnRebel();

            gameState.NextRebelSpawn = nextSpawnTick;

            spawnInterval = gameState.Mode.TickIntervalFactor * gameState.Mode.TickInterval / Mathf.Log(currentTime, gameState.Mode.TickIntervalLogBase);
            nextSpawnTick = currentTime + spawnInterval;
            //Debug.Log("Next Spawn: " + nextSpawnTick + "  interval: " + spawnInterval);
        }

        if (!gameState.Mode.DisableShop)
        {
            if (currentTime > nextMoneyTick)
            {
                nextMoneyTick = currentTime + moneyInterval;

                gameState.NextMoneySpawn = nextMoneyTick;

                gameState.AvailableCredits += gameState.Mode.MoneyGainPerInterval;
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
        if (GameHandler.SelectedTroop != null)
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

    public static Vector2 GetRandomTarget()
    {
        if (GameHandler.MilitaryBase==null || GameHandler.MilitaryBase.CoreMapBase.Destroyed)
        {
            return GameHandler.Palace.MapObject.Location;
        }
        else
        {
            var index = Mathf.FloorToInt(UnityEngine.Random.Range(0, 1.99f));
            if (index == 0)
            {
                return GameHandler.Palace.MapObject.Location;
            }
            return GameHandler.MilitaryBase.MapObject.Location;
        }
    }

    private SecurityForceBehaviour SpawnTroop(SecurityForce existingTroop = default)
    {
        var policeTroop = existingTroop;

        if (policeTroop == default)
        {
            policeTroop = GetTroopFromDefault(Core.Game.State.Mode.TroopDefaults.GetRandomEntry());
        }
        else
        {
            if (policeTroop.Location.x > 0.7) //If more Bases: Make Better check here
            {
                policeTroop.Base = GameHandler.MilitaryBase.CoreMapBase; // this should be loaded correctly
            }
            else
            {
                policeTroop.Base = GameHandler.Palace.CoreMapBase; // this should be loaded correctly
            }
        }

        GameObject troopOb = InstantiateGameObject(PoliceTroopTemplate, Map.transform);

        SecurityForceBehaviour troopBehaviour = troopOb.GetComponent<SecurityForceBehaviour>();

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

    private SecurityForce GetTroopFromDefault(TroopDefault troopDefault)
    {
        BaseDefault troopBase = troopDefault.Bases.GetRandomEntry();

        var policeTroop = new SecurityForce()
        {
            Name = troopDefault.Names.GetRandomEntry(),
            Speed = 0,
            MaxSpeed = troopDefault.MaxSpeed,
            Strength = troopDefault.Strength,
            Repulsion = troopDefault.Repulsion,
            Health = troopDefault.Health,
            MaxHealth = troopDefault.MaxHealth,
            Location = troopBase.Position.ToUnity(),
            ImageName = troopDefault.ImageNames.GetRandomEntry(),
            Range = troopDefault.Range,
            Base = GameHandler.Palace.CoreMapBase,
            MarchSounds = troopDefault.MarchSounds,
            ForegroundColor = troopDefault.ForegroundColor,
            BackgroundColor = troopDefault.BackgroundColor,
            SelectedColor = troopDefault.SelectedColor,
            MoveJustOnce = troopDefault.MoveJustOnce
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
            RebelDefault rebelDefault = Core.Game.State.Mode.RebelDefaults.GetRandomEntry();
            float speed = UnityEngine.Random.Range(rebelDefault.MinSpeed, rebelDefault.MaxSpeed);
            //Debug.Log("Speed: " + speed);
            rebel = new Rebel()
            {
                Name = GetRandomRebelName(),
                Speed = speed,
                Location = GetValidRandomLocation(),
                Target = GetRandomTarget(),
                ImageName = rebelDefault.ImageNames.GetRandomEntry(),
                Strength = rebelDefault.Strength,
                Repulsion = rebelDefault.Repulsion,
                Range = rebelDefault.Range,
                Health = rebelDefault.Health,
                MaxHealth = rebelDefault.MaxHealth,
                KillSound = rebelDefault.KillSounds.GetRandomEntry(),
                SpawnSound = rebelDefault.SpawnSounds.GetRandomEntry(),
            };

            Core.Game.State.Rebels.Add(rebel);

            Core.Game.AmbienceAudioManager.Resume();
            if (rebel.SpawnSound != default)
            {
                AudioClip spawnAudio = GameFrame.Base.Resources.Manager.Audio.Get(rebel.SpawnSound);
                Core.Game.EffectsAudioManager.Play(spawnAudio);
            }
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
        Vector2 location = default;
        float PalaceSafeZoneRadius = Core.Game.State.Mode.PalaceDefault.SafeZoneRadius;
        float? MilitarySafeZoneRadius = Core.Game.State.Mode.MilitaryBaseDefault?.SafeZoneRadius;

        while (true)
        {
            float locationX = UnityEngine.Random.Range(0f, 1f);
            float locationY = UnityEngine.Random.Range(0f, 1f);

            location = new Vector2(locationX, locationY);

            float distance = GameHandler.GetDistance(location, GameHandler.Palace.MapObject.Location);
            if (distance < PalaceSafeZoneRadius)
            {
                continue;
            }

            if (!Core.Game.State.Mode.DisableMilitaryBase)
            {
                distance = GameHandler.GetDistance(location, GameHandler.MilitaryBase.MapObject.Location);
                if (distance < MilitarySafeZoneRadius)
                {
                    continue;
                }
            }

            break;
        }

        return location;
    }

    private void InitMilitaryBase()
    {
        if (Core.Game.State.Mode.DisableMilitaryBase)
        {
            MilitaryBase.gameObject.SetActive(false);
        }
        else
        {
            GameHandler.MilitaryBase = MilitaryBase;

            if (Core.Game.State.MilitaryBase == null)
            {
                GameHandler.MilitaryBase.InitPalaceWithDefault(Core.Game.State.Mode.MilitaryBaseDefault);
            }
            else
            {
                GameHandler.MilitaryBase.InitPalace(Core.Game.State.MilitaryBase);
            }

            Core.Game.State.MilitaryBase = GameHandler.MilitaryBase.CoreMapBase;
        }
    }
    private void InitPalace()
    {
        GameHandler.Palace = Palace;
        GameHandler.Palace.InitPalace(Core.Game.State.Palace);

        Core.Game.State.Palace = GameHandler.Palace.CoreMapBase;
    }
}
