using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Vector2 limitMin;
    public Vector2 limitMax;
    public Action<Vector2, Vector2> PassLimitsToShip;
    public Action<float> PassLimitsToEnemy;
    public float speed;
    public List<Enemy>enemies = new List<Enemy>();

    void Start()
    {
        PassLimitsToShip(limitMin, limitMax);
        PassLimitsToEnemy(limitMin.y);
        enemies.AddRange(FindObjectsOfType<Enemy>());
    }

    private void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime);
    }
}
