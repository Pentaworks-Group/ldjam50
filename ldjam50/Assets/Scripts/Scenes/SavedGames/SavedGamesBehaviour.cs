using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scenes.SavedGames
{
    public class SavedGamesBehaviour : BaseMenuBehaviour
    {
        public List<SaveGameSlotBehaviour> SaveGameSlots;

        void Start()
        {
            var savedGamesJson = PlayerPrefs.GetString("SavedGames");

            if (!String.IsNullOrEmpty(savedGamesJson))
            {
                var savedGames = GameFrame.Core.Json.Handler.Deserialize<Assets.Scripts.Core.GameState[]>(savedGamesJson);

                if (savedGames?.Length > 0)
                {
                    Debug.Log($"Found GameStates: {savedGames.Length}");

                    for (int i = 0; i < 5; i++)
                    {
                        SaveGameSlots[i].GameState = savedGames[i];
                    }
                }
            }
        }


        public void OnSaveGameSlotClicked(SaveGameSlotBehaviour slot)
        {
            if (slot.GameState != default)
            {
                Assets.Scripts.Base.Core.Game.Start(slot.GameState);
            }
        }
    }
}
