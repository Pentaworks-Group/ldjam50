using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaceBehaviour : CoreMapObjectBehaviour
{
    public void InitPalace(CoreMapObject mapObject = default)
    {
        if (mapObject == default)
        {
            float locationX = (1895f / 3840);
            float locationY = 1f - (1043f / 2160);
            GameFrame.Core.Math.Vector2 location = new GameFrame.Core.Math.Vector2(locationX, locationY);

            mapObject = new CoreMapObject()
            {
                Name = "Palace",
                ActualLocation = location,
                ImageName = "Palace"
            };
            
        }
        Init(mapObject);
    }
}
