using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Base;


public class RebelBehaviour : CoreUnitBehaviour
{
    public void InitRebel(Rebel rebel)
    {
        sizeScale = 0.2f;
        AddDistanceAction(0.1f, CallGameOver);
        Init(rebel);
    }

    public void RebelClick()
    {
        GameObject.Destroy(gameObject);
    }

    private void CallGameOver(float distance)
    {
        Assets.Scripts.Base.Core.Game.ChangeScene(SceneNames.GameOver);
        Debug.Log("You have Lost. Looser!!");
    }
}

