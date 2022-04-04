﻿
using GameFrame.Core.Audio.Multi;
using GameFrame.Core.Audio.Single;

using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : GameFrame.Core.Game<GameState, PlayerOptions>
    {
        public ContinuousAudioManager AmbienceAudioManager { get; set; }
        public ContinuousAudioManager BackgroundAudioManager { get; set; }
        public EffectsAudioManager EffectsAudioManager { get; set; }
        
        public System.Collections.Generic.List<AudioClip> AudioClipListMenu { get; set; }
        public System.Collections.Generic.List<AudioClip> AudioClipListGame1 { get; set; }
        public System.Collections.Generic.List<AudioClip> AudioClipListGame2 { get; set; }
        public System.Collections.Generic.List<AudioClip> AudioClipListTransition { get; set; }
        public System.Collections.Generic.List<AudioClip> AmbientClipList { get; set; }

        public System.Collections.Generic.List<AudioClip> ShopClipList { get; set;}

        protected override GameState InitializeGameState()
        {
            return new GameState()
            {
                CurrentScene = SceneNames.City,
                NextRebelSpawn = GameHandler.GameFieldSettings.FirstTick,
                NextMoneySpawn = GameHandler.GameFieldSettings.MoneyFirstTick
            };
        }

        protected override PlayerOptions InitialzePlayerOptions()
        {
            return new PlayerOptions()
            {
                AreAnimationsEnabled = true,
                EffectsVolume = 1f,
                BackgroundVolume = 0.9f,
                AmbienceVolume = 0.125f
            };
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void GameStart()
        {
            Base.Core.Game.Startup();
        }

        public void PlayButtonSound()
        {
            EffectsAudioManager.Play("Button");
        }

    }
}
