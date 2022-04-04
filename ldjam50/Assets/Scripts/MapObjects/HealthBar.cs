using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private RectTransform RectTransformBar { get; set; }
    private CoreMapObject CoreMapObject { get; set; }
    private GameObject Display;

    void Start()
    {
        RectTransformBar = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        Display = gameObject.transform.GetChild(0).gameObject;
        LoadCoreMapObject();
    }

    private void LoadCoreMapObject()
    {
        CoreMapObjectBehaviour coreUnitBehaviour = this.gameObject.GetComponentInParent<CoreMapObjectBehaviour>();
        CoreMapObject = coreUnitBehaviour.MapObject;
    }

    void Update()
    {
        if (CoreMapObject == default)
        {
            LoadCoreMapObject();
        }
        if (CoreMapObject.Health >= CoreMapObject.MaxHealth)
        {
            if (Display.activeSelf)
            {
                Display.SetActive(false);
            }
        } else
        {
            if (!Display.activeSelf)
            {
                Display.SetActive(true);
            }
            float percentage = CoreMapObject.Health / CoreMapObject.MaxHealth;
            RectTransformBar.anchorMax = new Vector2(percentage, 1);
        }
    }
}
