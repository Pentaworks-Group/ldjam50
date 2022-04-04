using System;

using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenuBehavior : MonoBehaviour
{
    private Assets.Scripts.Core.GameState[] gameStates;
    public UnityEvent<Boolean> PauseToggled = new UnityEvent<Boolean>();

    public GameObject Menu;
    public GameObject GameView;
    public GameObject MenuArea;
    public GameObject OptionsArea;
    public GameObject SaveGameArea;
    public Button BackButton;
    public Button ContinueButton;
    public SaveGameSlotBehaviour[] SaveGameSlots;

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
                Core.Game.PlayButtonSound();
                Hide();

                this.PauseToggled.Invoke(false);
            }
        }
        else
        {
            Core.Game.PlayButtonSound();

            this.PauseToggled.Invoke(true);

            Show();
        }
    }

    public void ShowSavedGames()
    {
        Core.Game.PlayButtonSound();
        LoadGameStates();

        SetVisible(saveGame: true);
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

        Core.Game.AmbienceAudioManager.Stop();
    }

    public void OnBackButtonClicked()
    {
        Core.Game.PlayButtonSound();
        SetVisible(pauseMenu: true);
    }

    public void ShowOptions()
    {
        Core.Game.PlayButtonSound();
        this.SetVisible(options: true);
    }

    private void LoadGameStates()
    {
        Debug.Log("Loading saved games");
        var savedGames = PlayerPrefs.GetString("SavedGames");

        if (!String.IsNullOrEmpty(savedGames))
        {
            try
            {
                gameStates = GameFrame.Core.Json.Handler.Deserialize<Assets.Scripts.Core.GameState[]>(savedGames);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        if (gameStates == null)
        {
            Debug.Log("Couldn't parse string or none found.");
            gameStates = new Assets.Scripts.Core.GameState[5];
        }

        for (int i = 0; i < 5; i++)
        {
            SaveGameSlots[i].GameState = gameStates[i];
        }
    }

    public void OnSaveGameSlotClicked(SaveGameSlotBehaviour slot)
    {
        Core.Game.PlayButtonSound();
        var index = 0;

        for (int i = 0; i < 5; i++)
        {
            if (SaveGameSlots[i] == slot)
            {
                index = i;
                break;
            }
        }

        var serialized = GameFrame.Core.Json.Handler.Serialize(Core.Game.State);

        var gameState = GameFrame.Core.Json.Handler.Deserialize<Assets.Scripts.Core.GameState>(serialized);

        gameState.SavedOn = DateTime.Now;

        slot.GameState = gameState;
        gameStates[index] = gameState;

        Debug.Log("Serializing gameStates.");
        var savedGames = GameFrame.Core.Json.Handler.Serialize(gameStates);

        PlayerPrefs.SetString("SavedGames", savedGames);
        PlayerPrefs.Save();
    }

    public void Quit()
    {
        Time.timeScale = 1;

        Core.Game.PlayButtonSound();
        Core.Game.Stop();
        Core.Game.ChangeScene(SceneNames.MainMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            ToggleMenu();
        }
    }

    private void SetVisible(Boolean pauseMenu = false, Boolean options = false, Boolean saveGame = false)
    {
        this.MenuArea.SetActive(pauseMenu);
        this.OptionsArea.SetActive(options);
        this.SaveGameArea.SetActive(saveGame);

        this.ContinueButton.gameObject.SetActive(pauseMenu);
        this.BackButton.gameObject.SetActive(!pauseMenu);
    }
}
