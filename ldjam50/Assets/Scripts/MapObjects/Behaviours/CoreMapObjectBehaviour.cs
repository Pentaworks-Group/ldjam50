
using UnityEngine;
using UnityEngine.UI;

public abstract class CoreMapObjectBehaviour : MonoBehaviour
{
    public Image Image { get; private set; }
    public Image BackgroundImage { get; private set; }
    public RectTransform RectTransform { get; set; }

    public CoreMapObject MapObject { get; set; }

    private float halfImageSizeRelativeX;
    private float halfImageSizeRealtiveY;

    protected float sizeScale = 1f;

    protected void Init(CoreMapObject mapObject)
    {
        RectTransform = this.gameObject.GetComponent<RectTransform>();
        BackgroundImage = this.gameObject.transform.Find("Active/BackgroundImage").GetComponent<Image>();
        Image = this.gameObject.transform.Find("Active/Image").GetComponent<Image>();

        RectTransform.offsetMin = new Vector2(0, 0);
        RectTransform.offsetMax = new Vector2(0, 0);

        gameObject.name = mapObject.Name;

        Sprite sprite = GameFrame.Base.Resources.Manager.Sprites.Get(mapObject?.ImageName);
        Image.sprite = sprite;
        MapObject = mapObject;
        InitScales();

        SetLocation(mapObject.Location);
    }

    protected void InitScales()
    {
        if (Image?.sprite != null)
        {
            halfImageSizeRelativeX = sizeScale * (Image.sprite.rect.width / 3840) / 2f;
            halfImageSizeRealtiveY = sizeScale * (Image.sprite.rect.height / 2160) / 2f;
        }
    }

    protected void SetLocation(Vector2 location)
    {
        if (RectTransform != null)
        {
            RectTransform.anchorMin = new Vector2(location.x - halfImageSizeRelativeX, location.y - halfImageSizeRealtiveY);
            RectTransform.anchorMax = new Vector2(location.x + halfImageSizeRelativeX, location.y + halfImageSizeRealtiveY);

            MapObject.Location = location;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageObject(float damage)
    {
        MapObject.Health -= damage;
        //Debug.Log("Health: " + CoreUnit.Health + "/" + CoreUnit.MaxHealth + " Damage received: " + damage);
        if (MapObject.Health <= 0)
        {
            KillObject();
        }
    }

    public abstract bool IsMoveable();

    public virtual void MoveInDirection(Vector2 direction)
    {
    }

    protected abstract void KillObject();

}
