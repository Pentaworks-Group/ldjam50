
using System;

using Assets.Scripts.Base;

using GameFrame.Core.Audio.Multi;
using GameFrame.Core.Audio.Single;

using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    public EffectsAudioManager EffectsAudioManager;
    public ContinuousAudioManager AmbienceAudioManager;
    public ContinuousAudioManager BackgroundAudioManager;

    public GameObject MainMenuContainer;
    public GameObject OptionsMenuContainer;

    public GameObject SavedGamesButton;

    public GameObject BackButton;
    public GameObject QuitButton;

    public void StartGame()
    {
        Core.Game.Start();
        LoadGameFieldSettings();
    }

    public void ShowSavedGames()
    {
        Core.Game.ChangeScene(SceneNames.SavedGames);
    }

    public void ShowOptions()
    {
        ChangeContainerVisiblity(options: true);
    }

    public void ShowCredits()
    {
        Core.Game.ChangeScene(SceneNames.Credits);
    }

    public void Quit()
    {
        Assets.Scripts.Base.Core.Game.SaveOptions();
        Application.Quit();
    }

    public void Back()
    {
        ChangeContainerVisiblity(mainMenu: true);
    }

    private void ChangeContainerVisiblity(Boolean mainMenu = false, Boolean options = false)
    {
        if (!mainMenu && !options)
        {
            throw new InvalidOperationException("No visiblity is not allowed!");
        }

        this.MainMenuContainer.SetActive(mainMenu);
        this.OptionsMenuContainer.SetActive(options);

        this.QuitButton.SetActive(mainMenu);
        this.BackButton.SetActive(!mainMenu);
    }

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

        Core.Game.AudioClipListMenu = new System.Collections.Generic.List<AudioClip>()
        {
            GameFrame.Base.Resources.Manager.Audio.Get("Background_1")
        };

        Core.Game.AudioClipListGame1 = new System.Collections.Generic.List<AudioClip>()
        {
            GameFrame.Base.Resources.Manager.Audio.Get("Background_2"),
            GameFrame.Base.Resources.Manager.Audio.Get("Background_3"),
            GameFrame.Base.Resources.Manager.Audio.Get("Background_4")
        };

        Core.Game.AudioClipListGame2 = new System.Collections.Generic.List<AudioClip>()
        {
            GameFrame.Base.Resources.Manager.Audio.Get("Background_5"),
            GameFrame.Base.Resources.Manager.Audio.Get("Background_6"),
            GameFrame.Base.Resources.Manager.Audio.Get("Background_7"),
//            GameFrame.Base.Resources.Manager.Audio.Get("Background_8"),
            GameFrame.Base.Resources.Manager.Audio.Get("Background_9")
        };

        Core.Game.AudioClipListTransition = new System.Collections.Generic.List<AudioClip>()
        {
            GameFrame.Base.Resources.Manager.Audio.Get("Background_10")
        };

        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListMenu;

        //if (!Core.Game.AmbienceAudioManager.IsPlaying)
        //{
        //    Core.Game.AmbienceAudioManager.Resume();
        //}
        //else
        //{
        //    Core.Game.AmbienceAudioManager.Unmute();
        //}
    }

    private void LoadGameFieldSettings()
    {
        String filePath = Application.streamingAssetsPath + "/GameFieldSettings.json";

        StartCoroutine(GameFrame.Core.Json.Handler.DeserializeObjectFromStreamingAssets<GameFieldSettings>(filePath, SetGameFieldSettings));
    }
    private GameFieldSettings SetGameFieldSettings(GameFieldSettings gameFieldSettings)
    {
        GameHandler.GameFieldSettings = gameFieldSettings;
        Debug.Log("GameFieldSettings: " + gameFieldSettings.Name);
        return gameFieldSettings;
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

}
