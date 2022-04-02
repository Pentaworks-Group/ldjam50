using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaceBehaviour : CoreBehaviour
{
    public void InitPalace(CoreMapObject mapObject = default)
    {
        if (mapObject == default)
        {
            float locationX = (1895f / 3840);
            float locationY = 1 - (1043f / 2160);
            Vector2 location = new Vector2(locationX, locationY);

            mapObject = new CoreMapObject()
            {
                Name = "Palace",
                Location = location,
                ImageName = "Palace"
            };
            
        }
        Init(mapObject);
        Debug.Log("PalaceBehaviour");
    }
}
