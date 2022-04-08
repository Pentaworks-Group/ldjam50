using Assets.Scripts.Base;
using Assets.Scripts.Scenes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeMenuBehaviour : BaseMenuBehaviour
{
    public GameObject GameModeSlotTemplate;
    public GameObject CreateNewModeButton;
    private int MaxSlosts = 10;
    private List<GameModeSlotBehaviour> modeSlots = new List<GameModeSlotBehaviour>();
    private bool ownMode = false;
    // Start is called before the first frame update
    void Start()
    {
        LoadGlobalModes();
    }

    private void CreateAndFillSlot(int index, GameFieldSettings gameFieldSettings)
    {

        GameObject modeSlot = Instantiate(GameModeSlotTemplate, new Vector3(0, 0, 0), Quaternion.identity, GameModeSlotTemplate.transform.parent);
        float relative = 1f / MaxSlosts;
        RectTransform rect = modeSlot.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(rect.anchorMin.x, (float)index * relative);
        rect.anchorMax = new Vector2(rect.anchorMax.x, (float)(index + 1) * relative);
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);
        Text text = modeSlot.GetComponentInChildren<Text>();
        text.text = gameFieldSettings.Name;

        modeSlot.name = "GameModeSlot" + gameFieldSettings.Name;
        GameModeSlotBehaviour gameModeSlotBehaviour = modeSlot.GetComponent<GameModeSlotBehaviour>();
        gameModeSlotBehaviour.GameFieldSettings = gameFieldSettings;

        modeSlot.SetActive(true);
        modeSlots.Add(gameModeSlotBehaviour);
    }

    private void ClearModeSlots()
    {
        for (int i = modeSlots.Count - 1; i >= 0; i--)
        {
            GameObject modeSlot = modeSlots[i].gameObject;
            GameObject.Destroy(modeSlot);
            modeSlots.RemoveAt(i);
        }
    }

    public void SaveGameModes()
    {
        if (ownMode)
        {
            List<GameFieldSettings> ownModes = getModesFromSlots();
            LoadGameModes(ownModes);
            String ownModesJson = GameFrame.Core.Json.Handler.Serialize(ownModes);
            PlayerPrefs.SetString("OwnModes", ownModesJson);
            PlayerPrefs.Save();
        }
    }


    private void LoadGameModes(List<GameFieldSettings> modes)
    {
        ClearModeSlots();
        for (int i = 0; i < modes.Count; i++)
        {
            CreateAndFillSlot(i, modes[i]);
        }
    }

    public void CreateNewMode()
    {
        if (modeSlots.Count < 10)
        {
            GameFieldSettings gameFieldSettings = new GameFieldSettings();
            List<GameFieldSettings> ownModes = getModesFromSlots();
            ownModes.Add(gameFieldSettings);
            LoadGameModes(ownModes);
        }
    }

    public void LoadGlobalModes()
    {
        ownMode = false;
        CreateNewModeButton.SetActive(false);
        LoadGameModes(GameHandler.AvailableGameModes);
    }

    public void LoadOwnModes()
    {
        ownMode = true;
        CreateNewModeButton.SetActive(true);
        String ownModesJson = PlayerPrefs.GetString("OwnModes");
        List<GameFieldSettings> ownModes;
        if (!String.IsNullOrEmpty(ownModesJson))
        {
            ownModes = GameFrame.Core.Json.Handler.Deserialize<List<GameFieldSettings>>(ownModesJson);
        } else
        {
            ownModes = new List<GameFieldSettings>();
        }

        LoadGameModes(ownModes);
    }

    private List<GameFieldSettings> getModesFromSlots()
    {
        List<GameFieldSettings> modes = new List<GameFieldSettings>();
        foreach(GameModeSlotBehaviour slot in modeSlots)
        {
            modes.Add(slot.GameFieldSettings);
        }
        return modes;
    }
}
