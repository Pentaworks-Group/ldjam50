
using System;

using UnityEngine;
using UnityEngine.UI;

public class KeyValueTextBehaviour : MonoBehaviour
{
    private Text keyText;
    private Text valueText;

    public String Key;
    public String Value;

    // Start is called before the first frame update
    void Start()
    {
        keyText = this.gameObject.transform.Find("KeyText").GetComponent<Text>();
        valueText = this.gameObject.transform.Find("ValueText").GetComponent<Text>();

        this.keyText.text = "";
        this.valueText.text = "";
    }

    private void Update()
    {
        if (this.Key != this.keyText.text)
        {
            this.keyText.text = Key;
        }

        if (this.Value != this.valueText.text)
        {
            this.valueText.text = Value;
        }
    }
}
