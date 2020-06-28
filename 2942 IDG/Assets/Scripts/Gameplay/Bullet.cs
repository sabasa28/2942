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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Enemy Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
