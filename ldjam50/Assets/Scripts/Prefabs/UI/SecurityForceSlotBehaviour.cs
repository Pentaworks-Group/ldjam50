
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

    private PoliceTroop policeTroop;
    public PoliceTroop PoliceTroop
    {
        get { return policeTroop; }
        set
        {
            if (policeTroop != value)
            {
                this.policeTroop = value;
                UpdateUI();
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

        UpdateUI();
    }

    private void UpdateUI()
    {
        //this.corpsImage.sprite = GetSprite(this.PoliceTroop?.ImageName);
        this.corpsNameText.text = this.PoliceTroop?.Name;
        this.strengthKeyValue.Value = this.PoliceTroop?.Strength.ToString("F1");
        this.maxSpeedKeyValue.Value = this.PoliceTroop?.MaxSpeed.ToString("F1");
        this.maxHealthKeyValue.Value = this.PoliceTroop?.MaxHealth.ToString("F1");
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
