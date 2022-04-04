using Assets.Scripts.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonBehaviour : MonoBehaviour
{
    public Text text;
      void Update()
    {
        if (Core.SelectedGameMode != default)
        {
            text.text = Core.SelectedGameMode.Name;
        }
    }
}
