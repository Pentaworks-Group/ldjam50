using System;

using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour
{
    public Text ElapesedTimeText;

    // Start is called before the first frame update
    void Start()
    {
        Core.Game.BackgroundAudioManager.Stop();
        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListTransition;
        Core.Game.BackgroundAudioManager.Resume();
        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListMenu;

        this.ElapesedTimeText.text = String.Format("You managed to stay in power for {0:F1} seconds.", Core.Game.State.ElapsedTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        Core.Game.PlayButtonSound();
        Assets.Scripts.Base.Core.Game.Stop();

        Core.Game.ChangeScene(SceneNames.MainMenu);
    }
}