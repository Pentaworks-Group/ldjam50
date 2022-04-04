using System;
using System.Collections.Generic;

using UnityEngine;

public abstract class CoreUnitBehaviour : CoreMapObjectBehaviour
{
    private Dictionary<float, Action<float>> distanceActions = new Dictionary<float, Action<float>>();

    private CoreUnit unit;
    public CoreUnit CoreUnit
    {
        get
        {
            return this.GetUnit<CoreUnit>();
        }
        private set
        {
            this.unit = value;

            UpdateUI();
        }
    }

    protected T GetUnit<T>() where T : CoreUnit
    {
        return (T)this.unit;
    }

    protected void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (CoreUnit.Speed == 0 || MapObject == null)
        {
            return;
        }

        Vector2 direction = CoreUnit.Target - MapObject.Location;

        direction.Normalize();

        direction *= CoreUnit.Speed * Time.deltaTime;
        MoveInDirection(direction);
        //Debug.Log("Move: " + newLocation + " direction: " + direction);
    }

    public override void MoveInDirection(Vector2 direction)
    {
        var unityLocation = MapObject.Location;

        Vector2 newLocation = unityLocation + direction;

        if (this.Image != null)
        {
            if (direction.x < 0)
            {
                this.Image.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this.Image.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        SetLocation(newLocation);
        DistanzeTriggerCheck(unityLocation, CoreUnit.Target);
    }

    private void DistanzeTriggerCheck(Vector2 location, Vector2 target)
    {
        float distanceValue = GameHandler.GetDistance(location, target);
        foreach (KeyValuePair<float, Action<float>> entry in distanceActions)
        {
            if (entry.Key > distanceValue)
            {
                entry.Value.Invoke(distanceValue);
            }
        }
    }

    //    protected abstract void KillObject();

    protected void AddDistanceAction(float distance, Action<float> action)
    {
        distanceActions.Add(distance, action);
    }

    protected virtual void UpdateUI()
    {
        if (this.Image != null)
        {
            this.Image.sprite = this.GetSprite(this.CoreUnit?.ImageName);
        }
    }

    protected Sprite GetSprite(String imageName)
    {
        if (!String.IsNullOrEmpty(imageName))
        {
            return GameFrame.Base.Resources.Manager.Sprites.Get(imageName);
        }

        return null;
    }

    public void Init(CoreUnit unit)
    {
        CoreUnit = unit;
        base.Init(unit);
    }
}
