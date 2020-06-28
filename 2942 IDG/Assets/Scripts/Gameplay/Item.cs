using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    float timeBeforeDissapearing = 3.0f;
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeBeforeDissapearing);
        Destroy(gameObject);
    }
}
