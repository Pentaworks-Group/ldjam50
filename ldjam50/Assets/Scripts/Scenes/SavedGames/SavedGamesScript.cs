
using Assets.Scripts.Base;
using Assets.Scripts.Core;

using UnityEngine;

public class SavedGamesScript : MonoBehaviour
{
    public void LoadGame(GameState gameState)
    {
        if (gameState != default)
        {
            Core.Game.Start(gameState);
        }
    }

    public void Back()
    {
        Core.Game.ChangeScene(SceneNames.MainMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Load the saved games...
    }

    // Update is called once per frame
    void Update()
    {

    }
}
