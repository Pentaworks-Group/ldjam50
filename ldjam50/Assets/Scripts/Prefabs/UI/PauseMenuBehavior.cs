using System;
using System.Collections.Generic;

using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.UI;

public class PauseMenuBehavior : MonoBehaviour
{
    public GameObject Menu;
    public GameObject GameView;
    public GameObject MenuArea;
    public GameObject OptionsArea;
    public Button BackButton;
    public Button ContinueButton;

    public void ToggleMenu()
    {
        if (Menu.activeSelf == true)
        {
            if (this.OptionsArea.activeSelf)
            {
                this.OnBackButtonClicked();
            }
            else
            {
                Hide();

                Time.timeScale = 1;
                Core.Game.EffectsAudioManager?.Resume();
            }
        }
        else
        {
            Time.timeScale = 0;
            Core.Game.EffectsAudioManager?.Pause();

            Show();
        }
    }

    public void SaveGame()
    {
        Debug.Log("Loading saved games");
        var savedGames = PlayerPrefs.GetString("SavedGames");

        var gameStates = default(List<Assets.Scripts.Core.GameState>);

        if (!String.IsNullOrEmpty(savedGames))
        {
            try
            {
                gameStates = GameFrame.Core.Json.Handler.Deserialize<List<Assets.Scripts.Core.GameState>>(savedGames);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        if (gameStates == null)
        {
            Debug.Log("Couldn't parse string or none found.");
            gameStates = new List<Assets.Scripts.Core.GameState>();
        }

        Core.Game.State.SavedOn = DateTime.Now;
        gameStates.Add(Core.Game.State);

        Debug.Log("Serializing gameStates.");
        savedGames = GameFrame.Core.Json.Handler.Serialize(gameStates);

        PlayerPrefs.SetString("SavedGames", savedGames);
    }

    public void Hide()
    {
        Menu.SetActive(false);
    }

    public void Show()
    {
        CursorMode cursorMode = CursorMode.Auto;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);

        SetVisible(pauseMenu: true);

        Menu.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        SetVisible(pauseMenu: true);
    }

    public void ShowOptions()
    {
        this.SetVisible(options: true);
    }

    public void Quit()
    {
        Core.Game.Stop();
        Core.Game.ChangeScene(SceneNames.MainMenu);

        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void SetVisible(Boolean pauseMenu = false, Boolean options = false)
    {
        this.MenuArea.SetActive(pauseMenu);
        this.OptionsArea.SetActive(options);

        this.ContinueButton.gameObject.SetActive(pauseMenu);
        this.BackButton.gameObject.SetActive(!pauseMenu);
    }
}
