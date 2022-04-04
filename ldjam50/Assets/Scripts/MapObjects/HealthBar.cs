using System;

using UnityEngine;

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

    private Boolean LoadCoreMapObject()
    {
        CoreMapObjectBehaviour coreUnitBehaviour = this.gameObject.GetComponentInParent<CoreMapObjectBehaviour>();

        if (coreUnitBehaviour?.MapObject != null)
        {
            CoreMapObject = coreUnitBehaviour.MapObject;

            return true;
        }

        return false;
    }

    void Update()
    {
        if (CoreMapObject == null)
        {
            if (!LoadCoreMapObject())
            {
                return;
            }
        }

        if (CoreMapObject.Health >= CoreMapObject.MaxHealth)
        {
            if (Display.activeSelf)
            {
                Display.SetActive(false);
            }
        }
        else
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
