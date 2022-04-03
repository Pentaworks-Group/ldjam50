using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public abstract class CoreUnitBehaviour : CoreMapObjectBehaviour
{
    private Dictionary<float, Action<float>> distanceActions = new Dictionary<float, Action<float>>();

    protected Image image;

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
        }
    }

    protected T GetUnit<T>() where T : CoreUnit
    {
        return (T)this.unit;
    }

    private void Start()
    {
        if (this.image == null)
        {
            this.image = GetComponent<Image>();
        }
    }

    protected void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (CoreUnit.Speed == 0)
        {
            return;
        }

        Vector2 direction = CoreUnit.Target - MapObject.Location;

        direction.Normalize();

        direction *= CoreUnit.Speed * Time.deltaTime;
        MoveInDirection(direction);
        //Debug.Log("Move: " + newLocation + " direction: " + direction);
    }
    public void MoveInDirection(Vector2 direction)
    {
        var unityLocation = MapObject.Location;

        Vector2 newLocation = unityLocation + direction;

        if (direction.x < 0)
        {
            this.image.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.image.transform.localScale = new Vector3(1, 1, 1);
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

    public void DamageUnit(float damage)
    {
        CoreUnit.Health -= damage;
        //Debug.Log("Health: " + CoreUnit.Health + "/" + CoreUnit.MaxHealth + " Damage received: " + damage);
        if (CoreUnit.Health <= 0)
        {
            KillUnit();
        }
    }

    protected abstract void KillUnit();

    protected void AddDistanceAction(float distance, Action<float> action)
    {
        distanceActions.Add(distance, action);
    }

    public void Init(CoreUnit unit)
    {
        CoreUnit = unit;
        base.Init(unit);
    }
}
