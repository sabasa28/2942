using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    SpriteRenderer sr;
    float timeToTargetAlpha = 1.0f;
    float timeToDissapear = 1.0f; 

    Color origCol;
    Color targetCol;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        origCol = sr.color;
        targetCol = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        float t = 0.0f;
        while (sr.color.a != targetCol.a)
        {
            t += Time.deltaTime / timeToTargetAlpha;
            sr.color = Color.Lerp(origCol, targetCol, t);
            yield return null;
        }
        
        yield return new WaitForSeconds(timeToDissapear);
        t = 0;

        while (sr.color.a != origCol.a)
        {
            t += Time.deltaTime / timeToTargetAlpha;
            sr.color = Color.Lerp(targetCol, origCol, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
