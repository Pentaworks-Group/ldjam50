using System;
using System.Collections.Generic;

using Assets.Scripts.Core;

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
                var savedGames = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameState>>(savedGamesJson);

                if (savedGames?.Count > 0)
                {
                    Debug.Log($"Found GameStates: {savedGames.Count}");

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
