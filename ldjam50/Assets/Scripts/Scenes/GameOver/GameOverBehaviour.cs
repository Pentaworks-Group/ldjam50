using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Base;

public class GameOverBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Core.Game.BackgroundAudioManager.Stop();
        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListTransition;
        Core.Game.BackgroundAudioManager.Resume();
        Core.Game.BackgroundAudioManager.Clips = Core.Game.AudioClipListMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        Core.Game.ChangeScene(SceneNames.MainMenu);
    }

}