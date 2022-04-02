
using Assets.Scripts.Base;
using Assets.Scripts.Core;

using GameFrame.Core.Audio.Multi;
using GameFrame.Core.Audio.Single;

using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public EffectsAudioManager EffectsAudioManager;
    public ContinuousAudioManager AmbienceAudioManager;
    public ContinuousAudioManager BackgroundAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (Core.Game.EffectsAudioManager == default)
        {
            Core.Game.EffectsAudioManager = this.EffectsAudioManager;
            Core.Game.EffectsAudioManager.Volume = Core.Game.Options.EffectsVolume;
            Core.Game.EffectsAudioManager.Initialize();
        }

        if (Core.Game.AmbienceAudioManager == default)
        {
            Core.Game.AmbienceAudioManager = this.AmbienceAudioManager;
            Core.Game.AmbienceAudioManager.Volume = Core.Game.Options.AmbienceVolume;
            Core.Game.AmbienceAudioManager.Initialize();
        }

        if (Core.Game.BackgroundAudioManager == default)
        {
            Core.Game.BackgroundAudioManager = this.BackgroundAudioManager;
            Core.Game.BackgroundAudioManager.Volume = Core.Game.Options.BackgroundVolume;
            Core.Game.BackgroundAudioManager.Initialize();
        }

        //if (!Core.Game.AmbienceAudioManager.IsPlaying)
        //{
        //    Core.Game.AmbienceAudioManager.Resume();
        //}
        //else
        //{
        //    Core.Game.AmbienceAudioManager.Unmute();
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    public void StartGame()
    {
        Core.Game.Start();
    }

    public void LoadGame(GameState loadedGamestate)
    {
        if (loadedGamestate != default)
        {
            Core.Game.Start(loadedGamestate);
        }
    }
}
