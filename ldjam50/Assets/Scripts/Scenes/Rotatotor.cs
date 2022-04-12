using UnityEngine;

public class Rotatotor : MonoBehaviour
{
    void Start()
    {
        if (Screen.width < Screen.height)
        {
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.rotation = new Quaternion(0, 0, 1, 1f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(Screen.height, Screen.width);
        }
    }
}
