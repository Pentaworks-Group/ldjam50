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
            location = new GameFrame.Core.Math.Vector2(GameHandler.GameFieldSettings.PalaceDefault.Pos_x, GameHandler.GameFieldSettings.PalaceDefault.Pos_y);

            mapBaseObject = new CoreMapBase()
            {
                Name = "Palace",
                ActualLocation = location,
                ImageName = "Palace",
                Healing = GameHandler.GameFieldSettings.PalaceDefault.Healing,
                Health = GameHandler.GameFieldSettings.PalaceDefault.Health,
                MaxHealth = GameHandler.GameFieldSettings.PalaceDefault.MaxHealth
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
        }

        if (Core.Game.State != default)
        {
            Core.Game.AmbienceAudioManager.Volume = (1.0f - 2 * min_distance) * Core.Game.Options.AmbienceVolume;
        }
    }
}
