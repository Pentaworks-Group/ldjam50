using Assets.Scripts.Core;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveGameSlotBehaviour : MonoBehaviour
{
    public Text SavedOnText;
    public Text ElapsedOnText;
    public GameObject EmptyContainer;
    public GameObject UsedContainer;

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
            this.ElapsedOnText.text = string.Format("{0:F1}s", this.GameState.ElapsedTime);

            EmptyContainer.SetActive(false);
            UsedContainer.SetActive(true);
        }
        else
        {
            this.SavedOnText.text = "";
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
