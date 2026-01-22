using UnityEngine;
using System.Collections;

public class OniRespawn_t : MonoBehaviour
{
    public float respawnDelay = 2.0f;

    bool isRespawning = false;
    Coroutine respawnRoutine;

    SpriteRenderer sr;
    Collider2D col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Die()
    {
        if (isRespawning) return;
        respawnRoutine = StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        if (sr) sr.enabled = false;
        if (col) col.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        ForceReset();
    }

    // Åö Ç¢Ç¬Ç≈Ç‡èâä˙èÛë‘Ç…ñﬂÇ∑ÇΩÇﬂÇÃä÷êî
    public void ForceReset()
    {
        if (respawnRoutine != null)
        {
            StopCoroutine(respawnRoutine);
            respawnRoutine = null;
        }

        isRespawning = false;

        if (sr)
        {
            sr.enabled = true;
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }

        if (col) col.enabled = true;
    }
}
