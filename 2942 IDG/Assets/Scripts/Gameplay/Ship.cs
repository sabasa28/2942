using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Explotion explotionPrefab;
    public float speed;
    public Vector3 offsetForBulletSpawn;
    public float timeBetweenShots;
    bool canShoot = true;
    public float energy;
    const float powerShotsDuration = 10.0f;
    bool poweredShots = false;
    Coroutine PowerBuff = null;
    Vector2 levelLimitsMin;
    Vector2 levelLimitsMax;
    float energyLostOnCollision = 10;
    int bombs = 1;
    void Awake()
    {
        FindObjectOfType<Level>().PassLimitsToShip = GetLimits;
        StartCoroutine(LoseEnergy());
        StartCoroutine(CheckWin());
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

        if (Input.GetKey(KeyCode.Space) && canShoot && Time.timeScale != 0)
        {
            Bullet bulletInstance = Instantiate(bulletPrefab, transform.position + offsetForBulletSpawn, Quaternion.identity);
            bulletInstance.levelLimit = levelLimitsMax.y;
            if (poweredShots) bulletInstance.transform.localScale *= 2;
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.Z) && Time.timeScale != 0 && bombs > 0)
        {
            Instantiate(explotionPrefab, transform.position, Quaternion.identity);
            bombs--;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                SceneManager.LoadScene(3, LoadSceneMode.Additive);
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync(3);
            }
        }
    }

    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    IEnumerator LoseEnergy()
    {
        while (energy > 0)
        {
            energy -= 1;
            CheckEnergy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator PowerShots()
    {
        poweredShots = true;
        yield return new WaitForSeconds(powerShotsDuration);
        poweredShots = false;
    }

    IEnumerator CheckWin()
    {
        yield return new WaitForSeconds(35.0f);
        Debug.Log("YOU FINISHED! YOU WON");
        SceneManager.LoadScene(2);
    }

    public void GetLimits(Vector2 minLimit, Vector2 maxLimit)
    {
        levelLimitsMin = minLimit;
        levelLimitsMax = maxLimit;
    }

    void CheckEnergy()
    {
        if (energy <= 0)
        {
            Debug.Log("RUN OUT OF ENERGY! GAME OVER");
            SceneManager.LoadScene(2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy Bullet"))
        {
            energy -= energyLostOnCollision;
            CheckEnergy();
        }
        if (collision.gameObject.CompareTag("Power Item"))
        {
            if (PowerBuff != null)
                StopCoroutine(PowerBuff);
            PowerBuff = StartCoroutine(PowerShots());
        }
        if (collision.gameObject.CompareTag("Energy Item"))
        {
            energy += 20;  
        }
    }

    
}
