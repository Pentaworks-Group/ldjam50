using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Base;
using System;
using GameFrame.Core.Extensions;

public class RebelBehaviour : CoreUnitBehaviour
{
    public void Init(Rebel rebel)
    {
        sizeScale = 0.2f;
//        AddDistanceAction(rebel.Range, CallGameOver);
        base.Init(rebel);
    }

    public Rebel Rebel
    {
        get
        {
            return this.GetUnit<Rebel>();
        }
    }

    public void RebelClick()
    {
        //KillRebel();
    }


    private void CallGameOver(float distance)
    {
        Core.Game.AmbienceAudioManager.Stop();
        Assets.Scripts.Base.Core.Game.ChangeScene(SceneNames.GameOver);
        Debug.Log("You have Lost. Looser!! " + distance);
    }

   

    internal void Repel(float distance, CoreUnitBehaviour opponent)
    {
        GameHandler.Repel(opponent, this, distance);
    }

    internal void Fight(float distance, CoreUnitBehaviour opponent)
    {
        GameHandler.Fight(opponent, this, distance);
    }

    protected override void KillUnit()
    {
        GameHandler.RemoveRebel(this);
        GameObject.Destroy(gameObject);

        AudioClip clip = GameFrame.Base.Resources.Manager.Audio.Get(Rebel.KillSound);
        Core.Game.EffectsAudioManager.Play(clip);
        if (Core.Game.State.Rebels.Count <= 0)
        {
            Core.Game.AmbienceAudioManager.Stop();
        }
    }
}

