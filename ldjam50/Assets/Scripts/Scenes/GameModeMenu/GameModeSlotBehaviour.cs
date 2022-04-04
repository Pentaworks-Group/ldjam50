using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSlotBehaviour : MonoBehaviour
{
    public GameFieldSettings GameFieldSettings { get; set; }

    public void OnSlotClick()
    {
        Assets.Scripts.Base.Core.SelectedGameMode = GameFieldSettings;
        Core.Game.PlayButtonSound();
        Core.Game.ChangeScene(SceneNames.MainMenu);
    }
}
