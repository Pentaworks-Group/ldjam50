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

    private void KillRebel()
    {
        GameHandler.RemoveRebel(this);
        GameObject.Destroy(gameObject);
    }

    private void CallGameOver(float distance)
    {
        Assets.Scripts.Base.Core.Game.ChangeScene(SceneNames.GameOver);
        Debug.Log("You have Lost. Looser!! " + distance);
    }

    internal void Repel(float distance, PoliceTroop policeTroop)
    {
        Vector2 direction = MapObject.Location - policeTroop.Location;
        direction.Normalize();
        direction *= 0.22f * Time.deltaTime;
        MoveInDirection(direction);
    }

    internal void Fight(float distance, PoliceTroop policeTroop)
    {
        throw new NotImplementedException();
    }
}

