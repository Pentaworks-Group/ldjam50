using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorBehaviour : MonoBehaviour
{
    private TMPro.TMP_InputField inputField;
    private GameModeSlotBehaviour openSlot;

    public void ToggleEditor()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }


    public void OpenGameFieldSettings(GameModeSlotBehaviour slotToEdit)
    {
        if (inputField == default)
        {
             inputField = transform.GetChild(0).GetComponent<TMPro.TMP_InputField>();
            //GameObject go2 = transform.Find("InputField/TextArea/Text").gameObject;
            //Debug.Log(go2);
            //inputField = transform.Find("InputField/TextArea/Text").GetComponent<Text>();
        }
        gameObject.SetActive(true);
        openSlot = slotToEdit;
        inputField.text = GameFrame.Core.Json.Handler.SerializePretty(slotToEdit.GameFieldSettings);
    }

    public void SaveSettings()
    {
        openSlot.GameFieldSettings = GameFrame.Core.Json.Handler.Deserialize<GameFieldSettings>(inputField.text);
    }


}
