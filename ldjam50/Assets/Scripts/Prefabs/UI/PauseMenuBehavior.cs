using System;

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

    public void Hide()
    {
        Menu.SetActive(false);
        GameView.SetActive(true);
    }

    public void Show()
    {
        CursorMode cursorMode = CursorMode.Auto;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);

        SetVisible(pauseMenu: true);

        Menu.SetActive(true);
        GameView.SetActive(false);
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
