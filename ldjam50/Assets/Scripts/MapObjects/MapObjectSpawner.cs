using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapObjectSpawner : MonoBehaviour
{
    public PalaceBehaviour palace;

    // Start is called before the first frame update
    void Start()
    {
        InitPalace();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void InitPalace()
    {
        palace.InitPalace();   
        Debug.Log("InitPalace");
    }




}
