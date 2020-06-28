using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Ship : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float speed;
    public Vector3 offsetForBulletSpawn = new Vector3(0, 2.0f);
    public float timeBetweenShots;
    bool canShoot = true;
    Vector2 levelLimitsMin;
    Vector2 levelLimitsMax;

    void Awake()
    {
        FindObjectOfType<Level>().PassLimitsToShip = GetLimits;
    }

    void Update()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(movementX, movementY) * Time.deltaTime * speed;

        Vector3 newPos = transform.position + movement;

        if (newPos.x > levelLimitsMin.x && newPos.x < levelLimitsMax.x)
            transform.position += new Vector3 (movement.x,0);
        if (newPos.y > levelLimitsMin.y && newPos.y < levelLimitsMax.y)
            transform.position += new Vector3(0, movement.y);

        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            Instantiate(bulletPrefab, transform.position + offsetForBulletSpawn, Quaternion.identity).levelLimit = levelLimitsMax.y;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    public void GetLimits(Vector2 minLimit, Vector2 maxLimit)
    {
        levelLimitsMin = minLimit;
        levelLimitsMax = maxLimit;
    }
}
