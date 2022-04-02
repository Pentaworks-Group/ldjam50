
using UnityEngine;
using UnityEngine.UI;

public class CoreBehaviour : MonoBehaviour
{
    public Image Image { get; set; }
    public RectTransform RectTransform { get; set; }

    public CoreMapObject MapObject { get; set; }

    private float halfImageSizeRelativeX;
    private float halfImageSizeRealtiveY;

    protected void Init(CoreMapObject mapObject)
    {
        RectTransform = this.gameObject.GetComponent<RectTransform>();
        Image = this.gameObject.GetComponent<Image>();

        gameObject.name = mapObject.Name;

        Debug.Log(mapObject.Location);

        Sprite sprite = GameFrame.Base.Resources.Manager.Sprites.Get(mapObject.ImageName);
        Image.sprite = sprite;
        MapObject = mapObject;
        InitScales();

        SetLocation(mapObject.Location);
        Debug.Log("CoreBehaviour");
    }

    protected void InitScales()
    {
        halfImageSizeRelativeX = (Image.sprite.rect.width / 3840) / 2f;
        halfImageSizeRealtiveY = (Image.sprite.rect.height / 2160) / 2f;
    }

    protected void SetLocation(Vector2 location)
    {
        RectTransform.anchorMin = new Vector2(location.x - halfImageSizeRelativeX, location.y - halfImageSizeRealtiveY);
        RectTransform.anchorMax = new Vector2(location.x + halfImageSizeRelativeX, location.y + halfImageSizeRealtiveY);
        MapObject.Location = location;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
