using System;

using Assets.Scripts.Base;

using UnityEngine;


public class PalaceBehaviour : CoreMapObjectBehaviour
{
    public CoreMapBase CoreMapBase { get; set; }

    public float Healing { get; set; }

    private GameFrame.Core.Math.Vector2 location;

    public void InitPalace(CoreMapBase mapBaseObject = default)
    {
        if (mapBaseObject == default)
        {
            float locationX = (1895f / 3840);
            float locationY = 1f - (1043f / 2160);
            location = new GameFrame.Core.Math.Vector2(locationX, locationY);

            mapBaseObject = new CoreMapBase()
            {
                Name = "Palace",
                ActualLocation = location,
                ImageName = "Palace",
                Healing = GameHandler.GameFieldSettings.PalaceHealing,
                Health = 10,
                MaxHealth = 10
            };

        }
        CoreMapBase = mapBaseObject;
        Init(mapBaseObject);
    }

    public void Update()
    {
        updateRebelDistance();
    }
    private void updateRebelDistance()
    {
        float min_distance = 1000.0f;
        for (int i = 0; i < GameHandler.Rebels.Count; i++)
        {
            RebelBehaviour rebel = GameHandler.Rebels[i];
            float distance = GameHandler.GetDistance(rebel.MapObject.Location, new Vector2(location.X, location.Y));
            min_distance = Math.Min(distance, min_distance);

            //Attack Palace/Base
            GameHandler.Fight(rebel, this, distance);
        }

        if (Core.Game.State != default)
        {
            Core.Game.AmbienceAudioManager.Volume = (1.0f - 2 * min_distance) * Core.Game.Options.AmbienceVolume;
        }
    }

    protected override void KillObject()
    {
        CallGameOver(0.0f);
    }

    private void CallGameOver(float distance)
    {
        Core.Game.AmbienceAudioManager.Stop();
        Assets.Scripts.Base.Core.Game.ChangeScene(SceneNames.GameOver);
        Debug.Log("You have Lost. Looser!! " + distance);
    }
}
