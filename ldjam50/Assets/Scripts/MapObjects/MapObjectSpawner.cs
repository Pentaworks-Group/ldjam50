using System;
using System.Collections.Generic;
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


    private PoliceTroopBehaviour selectedTroop;

    private float nextTick = 3;
    private float spawnInterval = 30;
    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameHandler.Clear();
        InitPalace();
<<<<<<< Updated upstream
        selectedTroop = SpawnTroop();
=======
        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListGame1;
>>>>>>> Stashed changes
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
        UpdateTimeDisplay();
    }

    public void MoveSelectedTroop(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        float relPositionX = pointerData.position.x / Screen.width;
        float relPositionY = pointerData.position.y / Screen.height;
        selectedTroop.SendTroopsToLocation(new Vector2(relPositionX, relPositionY));
    }

    private void UpdateTimeDisplay()
    {
        TimeDisplay.text = currentTime.ToString("F1");
    }

    private PoliceTroopBehaviour SpawnTroop()
    {
        PoliceTroop policeTroop = new PoliceTroop()
        {
            Name = "Police Troop",
            Speed = 0,
            MaxSpeed = 0.4f,
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
        float speed = UnityEngine.Random.Range(0.05f, 0.1f);
        //Debug.Log("Speed: " + speed);
        Rebel rebel = new Rebel()
        {
            Name = GetRandomRebelName(),
            Speed = speed,
            Location = GetValidRandomLocation(),
            Target = GameHandler.Palace.MapObject.Location,
            ImageName = "Protest"
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
        float locationX = UnityEngine.Random.Range(0f, 1f);
        float locationY = UnityEngine.Random.Range(0f, 1f);
        Vector2 location = new Vector2(locationX, locationY);

        return location;
    }

    private void InitPalace()
    {
        GameHandler.Palace = Palace;
        GameHandler.Palace.InitPalace();
    }
}
