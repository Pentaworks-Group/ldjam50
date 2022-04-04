using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private RectTransform RectTransformBar { get; set; }
    private CoreMapObject CoreMapObject { get; set; }

    void Start()
    {
        RectTransformBar = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        CoreMapObjectBehaviour coreUnitBehaviour = this.gameObject.GetComponentInParent<CoreMapObjectBehaviour>();
        CoreMapObject = coreUnitBehaviour.MapObject;
    }

    void Update()
    {
        GameObject display = gameObject.transform.GetChild(0).gameObject;
        if (CoreMapObject.Health >= CoreMapObject.MaxHealth)
        {
            if (display.activeSelf)
            {
                display.SetActive(false);
            }
        } else
        {
            if (!display.activeSelf)
            {
                display.SetActive(true);
            }
            float percentage = CoreMapObject.Health / CoreMapObject.MaxHealth;
            RectTransformBar.anchorMax = new Vector2(percentage, 1);
        }
    }
}
