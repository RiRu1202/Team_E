using UnityEngine;
using System.Collections;

public class HitodamaRespawn_t : MonoBehaviour
{
    public float respawnDelay = 2.0f;

    SpriteRenderer sr;
    Collider2D col;
    bool isRespawning = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Die()
    {
        if (isRespawning) return;
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        // šÁ‚¦‚½‚æ‚¤‚É‚·‚éi‚Å‚àGameObject‚Í~‚ß‚È‚¢j
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // š•œŠˆ
        if (sr != null) sr.enabled = true;
        if (col != null) col.enabled = true;

        isRespawning = false;
    }
}
