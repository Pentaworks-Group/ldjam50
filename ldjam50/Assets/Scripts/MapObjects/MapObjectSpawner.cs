using System;
using System.Collections.Generic;
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

    private float nextTick = 3;
    private float spawnInterval = 3f;
    private float currentTime = 0;

    private float musicChangeTick = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
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
            var troop = SpawnTroop();

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
        nextTick = Core.Game.State.NextSpawn;

        //        Core.Game.BackgroundAudioManager.Stop();
        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListGame1;
        //        Core.Game.BackgroundAudioManager.Resume();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > nextTick)
        {
            SpawnRebel();
            nextTick = currentTime + spawnInterval;

            Core.Game.State.NextSpawn = nextTick;

            spawnInterval *= 0.95f;
        }

        if (currentTime > musicChangeTick)
        {
            Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListGame2;
        }

        UpdateTimeDisplay();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            var troop = SpawnTroop();

            GameHandler.AddSecurityForce(troop);
            GameHandler.SelectTroop(troop);
        }
    }

    public void MoveSelectedTroop(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        float relPositionX = pointerData.position.x / Screen.width;
        float relPositionY = pointerData.position.y / Screen.height;

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

    private PoliceTroopBehaviour SpawnTroop(PoliceTroop existingTroop = default)
    {
        var policeTroop = existingTroop;

        if (policeTroop == default)
        {
            TroopDefault troopDefault = GameHandler.GameFieldSettings.TroopDefaults.GetRandomEntry();
            policeTroop = new PoliceTroop()
            {
                Name = troopDefault.Names.GetRandomEntry(),
                Speed = 0,
                MaxSpeed = troopDefault.MaxSpeed,
                Strength = troopDefault.Strength,
                Health = troopDefault.Health,
                MaxHealth = troopDefault.MaxHealth,
                Location = GameHandler.Palace.MapObject.Location,
                ImageName = troopDefault.ImageName,
                Base = GameHandler.Palace.MapObject
            };

            Core.Game.State.SecurityForces.Add(policeTroop);
        }
        else
        {
            policeTroop.Base = GameHandler.Palace.MapObject; // this should be loaded correctly
        }

        GameObject troopOb = Instantiate(PoliceTroopTemplate, new Vector3(0, 0, 0), Quaternion.identity, Map.transform);

        PoliceTroopBehaviour troopBehaviour = troopOb.GetComponent<PoliceTroopBehaviour>();

        troopBehaviour.gameObject.SetActive(true);
        troopBehaviour.Init(policeTroop);

        return troopBehaviour;
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
                Health = rebelDefault.Health,
                MaxHealth = rebelDefault.MaxHealth
            };

            Core.Game.State.Rebels.Add(rebel);
        }
    

        GameObject rebelOb = Instantiate(RebelTemplate, new Vector3(0, 0, 0), Quaternion.identity, Map.transform);

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

            float distance = Vector2.Distance(location, GameHandler.Palace.MapObject.Location);
            if (distance > GameHandler.SafeZoneRadius)
            {
                valid = true;
            }

        }

        return location;
    }

    private void InitPalace()
    {
        GameHandler.Palace = Palace;
        GameHandler.Palace.InitPalace();
    }
}


