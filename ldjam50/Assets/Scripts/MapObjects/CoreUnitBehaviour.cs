using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUnitBehaviour : CoreBehaviour
{
    protected CoreUnit coreUnit;
    private Dictionary<float, Action<float>> distanceActions = new Dictionary<float, Action<float>>();

    private void Update()
    {

        Move();

    }

    protected void Move()
    {
        DistanzeTriggerCheck(MapObject.Location, coreUnit.Target);
        Vector2 direction = coreUnit.Target - MapObject.Location;
        direction.Normalize();
        direction *= coreUnit.Speed * Time.deltaTime;
        Vector2 newLocation = MapObject.Location + direction;
        SetLocation(newLocation);

        //Debug.Log("Move: " + newLocation + " direction: " + direction);
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

    protected void AddDistanceAction(float distance, Action<float> action)
    {
        distanceActions.Add(distance, action);
    }

    public void Init(CoreUnit unit)
    {
        coreUnit = unit;
        base.Init(unit);
    }
}
