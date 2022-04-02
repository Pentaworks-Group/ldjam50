using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private RectTransform RectTransformBar { get; set; }
    private CoreUnit CoreUnit { get; set; }

    void Start()
    {
        RectTransformBar = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        CoreUnitBehaviour coreUnitBehaviour = this.gameObject.GetComponentInParent<CoreUnitBehaviour>();
        CoreUnit = coreUnitBehaviour.CoreUnit;
    }

    void Update()
    {
        float percentage = CoreUnit.Health / CoreUnit.MaxHealth;
        RectTransformBar.anchorMax = new Vector2(percentage, 1);
    }
}
