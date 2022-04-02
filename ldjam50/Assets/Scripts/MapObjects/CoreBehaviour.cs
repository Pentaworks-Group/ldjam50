
using UnityEngine;
using UnityEngine.UI;

public class CoreBehaviour : MonoBehaviour
{
    public Image Image { get; set; }
    public RectTransform RectTransform { get; set; }

    public CoreMapObject MapObject { get; set; }

    private float halfImageSizeRelativeX;
    private float halfImageSizeRealtiveY;

    protected float sizeScale = 1f;

    protected void Init(CoreMapObject mapObject)
    {
        RectTransform = this.gameObject.GetComponent<RectTransform>();
        Image = this.gameObject.GetComponent<Image>();

        RectTransform.offsetMin = new Vector2(0, 0);
        RectTransform.offsetMax = new Vector2(0, 0);


        gameObject.name = mapObject.Name;


        Sprite sprite = GameFrame.Base.Resources.Manager.Sprites.Get(mapObject.ImageName);
        Image.sprite = sprite;
        MapObject = mapObject;
        InitScales();

        SetLocation(mapObject.Location);
    }

    protected void InitScales()
    {
        halfImageSizeRelativeX = sizeScale * (Image.sprite.rect.width / 3840) / 2f;
        halfImageSizeRealtiveY = sizeScale * (Image.sprite.rect.height / 2160) / 2f;
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
