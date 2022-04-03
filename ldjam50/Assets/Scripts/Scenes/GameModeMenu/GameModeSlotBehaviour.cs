using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSlotBehaviour : MonoBehaviour
{
    public GameFieldSettings GameFieldSettings { get; set; }

    public void OnSlotClick()
    {
        GameHandler.GameFieldSettings = GameFieldSettings;
        Debug.Log("Selected Moode: " + GameFieldSettings.Name);
    }
}
