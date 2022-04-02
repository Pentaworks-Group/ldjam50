using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebelBehaviour : CoreUnitBehaviour
{
    public void InitRebel(Rebel rebel)
    {
        sizeScale = 0.2f;
        Init(rebel);
    }
}
