using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaceBehaviour : CoreMapObjectBehaviour
{

    public CoreMapBase CoreMapBase { get; set; }

    public float Healing { get; set; }

    public void InitPalace(CoreMapBase mapBaseObject = default)
    {
        if (mapBaseObject == default)
        {
            float locationX = (1895f / 3840);
            float locationY = 1f - (1043f / 2160);
            GameFrame.Core.Math.Vector2 location = new GameFrame.Core.Math.Vector2(locationX, locationY);

            mapBaseObject = new CoreMapBase()
            {
                Name = "Palace",
                ActualLocation = location,
                ImageName = "Palace",
                Healing = GameHandler.GameFieldSettings.PalaceHealing
            };
            
        }
        CoreMapBase = mapBaseObject;
        Init(mapBaseObject);
    }
}
