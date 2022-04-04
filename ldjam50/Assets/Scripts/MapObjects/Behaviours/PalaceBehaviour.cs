using System;

using Assets.Scripts.Base;

using GameFrame.Core.Extensions;

public class PalaceBehaviour : CoreMapObjectBehaviour
{
    public CoreMapBase CoreMapBase { get; set; }

    public float Healing { get; set; }

    private GameFrame.Core.Math.Vector2 location;

    public void InitPalace(CoreMapBase mapBaseObject = default)
    {
        if (mapBaseObject == default)
        {
            mapBaseObject = GetCoreMapBaseFromDefault(Core.Game.State.Mode.PalaceDefault);
        }

        CoreMapBase = mapBaseObject;
        Init(mapBaseObject);
    }

    public void InitPalaceWithDefault(PalaceDefault baseDefault)
    {
        CoreMapBase mapBaseObject = GetCoreMapBaseFromDefault(baseDefault);

        CoreMapBase = mapBaseObject;
        Init(mapBaseObject);
    }

    private CoreMapBase GetCoreMapBaseFromDefault(PalaceDefault baseDefault)
    {
        location = baseDefault.Position;

        return new CoreMapBase()
        {
            Name = baseDefault.Name,
            ActualLocation = location,
            ImageName = baseDefault.ImageNames.GetRandomEntry(),
            Healing = baseDefault.Healing,
            Health = baseDefault.Health,
            MaxHealth = baseDefault.MaxHealth,
            Range = baseDefault.Range,
            Repulsion = baseDefault.Repulsion,
            ObjectSize = baseDefault.ObjectSize,
            GameOverOnDestruction = baseDefault.GameOverOnDestruction
        };
    }

    public void Update()
    {
        if (MapObject != null)
        {
            updateRebelDistance();
        }
    }

    private void updateRebelDistance()
    {
        float min_distance = 1000.0f;
        for (int i = 0; i < GameHandler.Rebels.Count; i++)
        {
            RebelBehaviour rebel = GameHandler.Rebels[i];

            float distance = GameHandler.GetDistance(rebel.MapObject.Location, MapObject.Location/* new Vector2(location.X, location.Y)*/);
            min_distance = Math.Min(distance, min_distance);

            if (distance < MapObject.Range)
            {
                rebel.Repel(distance, this);
            }

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
        if (CoreMapBase.GameOverOnDestruction)
        {
            CallGameOver();
        }
    }

    public override bool IsMoveable()
    {
        return false;
    }


    private void CallGameOver()
    {
        Core.Game.AmbienceAudioManager.Stop();
        Assets.Scripts.Base.Core.Game.ChangeScene(SceneNames.GameOver);
    }
}
