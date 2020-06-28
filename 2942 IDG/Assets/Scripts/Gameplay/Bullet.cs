using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float levelLimit;

    void Update()
    {
        transform.position += new Vector3 (0, speed * Time.deltaTime);
        if (transform.position.y > levelLimit)
        {
            Destroy(gameObject);
        }
    }

}
