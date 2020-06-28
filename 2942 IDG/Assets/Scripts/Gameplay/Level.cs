using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Vector2 limitMin;
    public Vector2 limitMax;
    public Action<Vector2, Vector2> PassLimitsToShip;
    
    void Start()
    {
        PassLimitsToShip(limitMin, limitMax);
    }
}
