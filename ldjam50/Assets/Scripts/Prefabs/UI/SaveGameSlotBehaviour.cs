using Assets.Scripts.Core;

using UnityEngine;
using UnityEngine.UI;

public class SaveGameSlotBehaviour : MonoBehaviour
{
    public GameObject EmptyContainer;
    public GameObject UsedContainer;

    public Text SavedOnText;
    public Text ElapsedOnText;
    public Text GameModeText;

    public GameObject CreditsContainer;
    public Text CreditsText;


    private GameState gameState;
    public GameState GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            if (gameState != value)
            {
                gameState = value;

                this.UpdateUI();
            }
        }
    }

    private void UpdateUI()
    {
        if (gameState != null)
        {
            this.SavedOnText.text = string.Format("{0:G}", this.GameState.SavedOn);
            this.GameModeText.text = this.GameState.Mode.Name;

            if (!this.GameState.Mode.DisableShop)
            {
                this.CreditsContainer.SetActive(true);

                this.CreditsText.text = string.Format("{0:F2}", this.GameState.AvailableCredits);
            }
            else
            {
                this.CreditsContainer.SetActive(false);
            }

            this.ElapsedOnText.text = string.Format("{0:F1}s", this.GameState.ElapsedTime);

            EmptyContainer.SetActive(false);
            UsedContainer.SetActive(true);
        }
        else
        {
            this.SavedOnText.text = "";
            this.GameModeText.text = "";

            this.CreditsText.text = "";
            this.ElapsedOnText.text = "";

            EmptyContainer.SetActive(true);
            UsedContainer.SetActive(false);
        }
    }

    private void Update()
    {
        this.UpdateUI();
    }
}
