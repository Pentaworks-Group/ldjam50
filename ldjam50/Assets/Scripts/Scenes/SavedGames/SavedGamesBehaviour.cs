using System;

using UnityEngine;

namespace Assets.Scripts.Scenes.SavedGames
{
    public class SavedGamesBehaviour : BaseMenuBehaviour
    {
        void Start()
        {
            var savedGamesJson = PlayerPrefs.GetString("SavedGames");

            if (!String.IsNullOrEmpty(savedGamesJson))
            {
                var savedGames = GameFrame.Core.Json.Handler.Deserialize<Assets.Scripts.Core.GameState[]>(savedGamesJson);

                if (savedGames?.Length > 0)
                {
                    Debug.Log($"Found GameStates: {savedGames.Length}");

                    foreach (var gameState in savedGames)
                    {
                        Debug.Log($"GameState saved: {gameState.SavedOn}");
                    }
                }
                else
                {
                    Debug.Log($"No gamestates found");
                }
            }
        }
    }
}
