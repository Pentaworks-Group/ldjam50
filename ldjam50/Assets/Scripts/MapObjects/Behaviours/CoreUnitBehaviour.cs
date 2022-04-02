using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class CoreUnitBehaviour : CoreMapObjectBehaviour
{
    public CoreUnit CoreUnit { get; private set; }
    private Dictionary<float, Action<float>> distanceActions = new Dictionary<float, Action<float>>();

    protected void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (CoreUnit.Speed == 0 || CoreUnit.Target == default)
        {
            return;
        }
        Vector2 direction = CoreUnit.Target - MapObject.Location;
        direction.Normalize();
        direction *= CoreUnit.Speed * Time.deltaTime;
        MoveInDirection(direction);
        //Debug.Log("Move: " + newLocation + " direction: " + direction);
    }
    protected void MoveInDirection(Vector2 direction)
    {
        Vector2 newLocation = MapObject.Location + direction;
        SetLocation(newLocation);
        DistanzeTriggerCheck(MapObject.Location, CoreUnit.Target);
    }

    private void DistanzeTriggerCheck(Vector2 location, Vector2 target)
    {
        float distanceValue = Vector2.Distance(location, target);
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
