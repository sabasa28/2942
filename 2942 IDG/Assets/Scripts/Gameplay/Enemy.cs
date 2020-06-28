using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Vector3 offsetForBulletSpawn;
    public EnemyBullet bulletPrefab;
    float minTimeBetweenShots = 2.0f;
    float maxTimeBetweenShots = 2.0f;
    float levelLimit;

    private void Awake()
    {
        FindObjectOfType<Level>().PassLimitsToEnemy = GetLimits;
        StartCoroutine(Shoot());
    }
    private void Start()
    {
        //Vector3 rot = Quaternion.LookRotation(transform.position).eulerAngles;
        //Debug.Log(rot.normalized);
        //transform.forward = new Vector3(rot.x,0,rot.y);
    }
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime);
        if (transform.position.y < levelLimit)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenShots, maxTimeBetweenShots));
            Instantiate(bulletPrefab, transform.position + offsetForBulletSpawn, Quaternion.identity).levelLimit = levelLimit;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("PlayerBullet"))
            Destroy(gameObject);
    }

    public void GetLimits(float minLimit)
    {
        levelLimit = minLimit;
    }
}
