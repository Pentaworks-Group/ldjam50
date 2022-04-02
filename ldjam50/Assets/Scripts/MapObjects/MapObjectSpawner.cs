using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapObjectSpawner : MonoBehaviour
{
    public PalaceBehaviour palace;
    public GameObject Rebi;
    public RebelBehaviour rebelTemplate;
    public GameObject map;

    private List<RebelBehaviour> rebels = new List<RebelBehaviour>();

    private float nextTick = 2;
    private float spawnInterval = 3;
    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitPalace();
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
        UpdateMapObjects();
     }

    private void UpdateMapObjects()
    {

    }

    private void SpawnRebel()
    {
        float speed = UnityEngine.Random.Range(0, 5);
        Rebel rebel = new Rebel()
        {
            Name = GetRandomRebelName(),
            Speed = speed,
            Location = GetValidRandomLocation(),
            Target = palace.MapObject.Location,
            ImageName = "protest"
        };
        GameObject rebelOb = Instantiate<GameObject>(Rebi, new Vector3(0, 0, 0), Quaternion.identity, map.transform);
        RebelBehaviour rebelBehaviour = rebelOb.GetComponent<RebelBehaviour>();
        rebelBehaviour.gameObject.SetActive(true);
        rebelBehaviour.InitRebel(rebel);
        rebels.Add(rebelBehaviour);
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
        palace.InitPalace();   
        Debug.Log("InitPalace");
    }




}
