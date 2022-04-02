using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Base;
using System;

public class RebelBehaviour : CoreUnitBehaviour
{
    public void Init(Rebel rebel)
    {
        sizeScale = 0.2f;
        AddDistanceAction(0.08f, CallGameOver);
        base.Init(rebel);
    }

    public void RebelClick()
    {
        //KillRebel();
    }


    private void CallGameOver(float distance)
    {
        Assets.Scripts.Base.Core.Game.ChangeScene(SceneNames.GameOver);
        Debug.Log("You have Lost. Looser!! " + distance);
    }

    internal void Repel(float distance, CoreUnitBehaviour opponent)
    {
        Vector2 direction = MapObject.Location - opponent.MapObject.Location;
        direction.Normalize();
        direction *= 0.22f * Time.deltaTime;
        MoveInDirection(direction);
    }

    internal void Fight(float distance, CoreUnitBehaviour opponent)
    {
        GameHandler.Fight(opponent, this, distance);
    }

    protected override void KillUnit()
    {
        GameHandler.RemoveRebel(this);
        GameObject.Destroy(gameObject);
    }
}

