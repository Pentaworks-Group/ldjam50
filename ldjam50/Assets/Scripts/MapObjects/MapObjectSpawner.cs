using System;

using Assets.Scripts.Base;

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
        InitPalace();
        GameHandler.SelectedTroop = SpawnTroop();
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
            spawnInterval *= 0.95f;
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
        float relPositionX = pointerData.position.x / Screen.width;
        float relPositionY = pointerData.position.y / Screen.height;
        GameHandler.SelectedTroop.SendTroopsToLocation(new Vector2(relPositionX, relPositionY));
    }

    private void UpdateTimeDisplay()
    {
        TimeDisplay.text = currentTime.ToString("F1");
        Core.Game.State.ElapsedTime = currentTime;
    }

    private PoliceTroopBehaviour SpawnTroop()
    {
        PoliceTroop policeTroop = new PoliceTroop()
        {
            Name = "Police Troop",
            Speed = 0,
            MaxSpeed = 0.4f,
            Strength = 20,
            Health = 100,
            MaxHealth = 200,
            Location = GameHandler.Palace.MapObject.Location,
            ImageName = "Troops_P",
            Base = GameHandler.Palace.MapObject
        };
        GameObject troopOb = Instantiate(PoliceTroopTemplate, new Vector3(0, 0, 0), Quaternion.identity, Map.transform);

        PoliceTroopBehaviour troopBehaviour = troopOb.GetComponent<PoliceTroopBehaviour>();

        troopBehaviour.gameObject.SetActive(true);
        troopBehaviour.Init(policeTroop);
        return troopBehaviour;
    }

    private void SpawnRebel()
    {
        //float speed = 0;
        float speed = UnityEngine.Random.Range(0.05f, 0.1f);
        //Debug.Log("Speed: " + speed);
        Rebel rebel = new Rebel()
        {
            Name = GetRandomRebelName(),
            Speed = speed,
            Location = GetValidRandomLocation(),
            Target = GameHandler.Palace.MapObject.Location,
            ImageName = "Protest",
            Strength = 10,
            Health = 25,
            MaxHealth = 200
        };

        GameObject rebelOb = Instantiate(RebelTemplate, new Vector3(0, 0, 0), Quaternion.identity, Map.transform);

        RebelBehaviour rebelBehaviour = rebelOb.GetComponent<RebelBehaviour>();

        rebelBehaviour.gameObject.SetActive(true);
        rebelBehaviour.Init(rebel);

        GameHandler.AddRebel(rebelBehaviour);
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
            if (distance > GameHandler.safeZoneRadius)
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
