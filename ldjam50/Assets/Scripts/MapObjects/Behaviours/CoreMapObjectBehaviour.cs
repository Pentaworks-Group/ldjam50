
using UnityEngine;
using UnityEngine.UI;

public abstract class CoreMapObjectBehaviour : MonoBehaviour
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

    protected float getDistance(Vector2 location1, Vector2 location2)
    {
        Vector2 loc1 = new Vector2(location1.x * Screen.width, location1.y * Screen.height);
        Vector2 loc2 = new Vector2(location2.x * Screen.width, location2.y * Screen.height);
        float distance = Vector2.Distance(loc1, loc2) / Mathf.Sqrt(Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2));
        return distance;
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
