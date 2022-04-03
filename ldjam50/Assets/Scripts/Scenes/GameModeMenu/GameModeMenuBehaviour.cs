using Assets.Scripts.Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeMenuBehaviour : BaseMenuBehaviour
{
    public GameObject GameModeSlotTemplate;
    private int MaxSlosts = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameHandler.AvailableGameModes.Count; i++)
        {
            CreateAndFillSlot(i, GameHandler.AvailableGameModes[i]);
        }
    }

    private void CreateAndFillSlot(int index, GameFieldSettings gameFieldSettings)
    {

        GameObject modeSlot = Instantiate(GameModeSlotTemplate, new Vector3(0, 0, 0), Quaternion.identity, GameModeSlotTemplate.transform.parent);
        float relative = 1f / MaxSlosts;
        RectTransform rect = modeSlot.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(rect.anchorMin.x, (float) index * relative);
        rect.anchorMax = new Vector2(rect.anchorMax.x, (float) (index + 1) * relative);
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);
        Text text = modeSlot.GetComponentInChildren<Text>();
        text.text = gameFieldSettings.Name;

        modeSlot.name = "GameModeSlot" + gameFieldSettings.Name;
        GameModeSlotBehaviour gameModeSlotBehaviour = modeSlot.GetComponent<GameModeSlotBehaviour>();
        gameModeSlotBehaviour.GameFieldSettings = gameFieldSettings;

        modeSlot.SetActive(true);
    }



}
