using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUnitBehaviour : CoreBehaviour
{
    CoreUnit coreUnit;

    private void Update()
    {

        Move();

    }

    protected void Move()
    {
        Vector2 direction = coreUnit.Target - MapObject.Location;
        direction.Normalize();
        direction *= coreUnit.Speed * Time.deltaTime;
        Vector2 newLocation = MapObject.Location + direction;
        SetLocation(newLocation);

        Debug.Log("Move: " + newLocation + " direction: " + direction);
    }

    public void Init(CoreUnit unit)
    {
        coreUnit = unit;
        base.Init(unit);
    }
}
