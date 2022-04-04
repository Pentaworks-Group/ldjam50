
using System;

using UnityEngine;
using UnityEngine.UI;

public class SecurityForceSlotBehaviour : MonoBehaviour
{
    private Image corpsImage;
    private Text corpsNameText;
    private KeyValueTextBehaviour strengthKeyValue;
    private KeyValueTextBehaviour maxSpeedKeyValue;
    private KeyValueTextBehaviour maxHealthKeyValue;
    private KeyValueTextBehaviour rangeKeyValue;
    private Text unitCostText;
    
    private TroopDefault securityForceDefault;
    public TroopDefault SecurityForceDefault
    {
        get { return securityForceDefault; }
        set
        {
            if (securityForceDefault != value)
            {
                this.securityForceDefault = value;

                if (this.corpsImage != null)
                {
                    UpdateUI();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var gameObject = this.gameObject.transform.Find("ImageArea/CorpsImage");

        this.corpsImage = gameObject?.GetComponent<Image>();
        this.corpsNameText = this.gameObject.transform.Find("NameArea/CorpsName")?.GetComponent<Text>();
        this.strengthKeyValue = this.gameObject.transform.Find("DescriptionArea/StrengthKeyValue")?.GetComponent<KeyValueTextBehaviour>();
        this.maxSpeedKeyValue = this.gameObject.transform.Find("DescriptionArea/MaxSpeedKeyValue")?.GetComponent<KeyValueTextBehaviour>();
        this.maxHealthKeyValue = this.gameObject.transform.Find("DescriptionArea/MaxHealthKeyValue")?.GetComponent<KeyValueTextBehaviour>();
        this.rangeKeyValue = this.gameObject.transform.Find("DescriptionArea/RangeKeyValue")?.GetComponent<KeyValueTextBehaviour>();
        this.unitCostText = this.gameObject.transform.Find("CostArea/CostText")?.GetComponent<Text>();

        UpdateUI();
    }

    private void UpdateUI()
    {
        this.corpsImage.sprite = GetSprite(this.SecurityForceDefault?.ImageName);

        this.corpsNameText.text = this.SecurityForceDefault?.Type;
        this.strengthKeyValue.Value = this.SecurityForceDefault?.Strength.ToString("F1");
        this.maxSpeedKeyValue.Value = this.SecurityForceDefault?.MaxSpeed.ToString("F1");
        this.maxHealthKeyValue.Value = this.SecurityForceDefault?.MaxHealth.ToString("F1");
        this.rangeKeyValue.Value = this.SecurityForceDefault?.Range.ToString("F1");
        this.unitCostText.text = this.SecurityForceDefault?.UnitCost.ToString("F1");
    }

    private Sprite GetSprite(String resourceName)
    {
        if (!String.IsNullOrEmpty(resourceName))
        {
            return GameFrame.Base.Resources.Manager.Sprites.Get(resourceName);
        }

        return default;
    }
}
