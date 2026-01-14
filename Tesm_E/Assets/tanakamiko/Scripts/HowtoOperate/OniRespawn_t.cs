using UnityEngine;
using System.Collections;

public class OniRespawn_t : MonoBehaviour
{
    public float respawnDelay = 2.0f;

    bool isRespawning = false;

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
        isRespawning = true;

        // 見えなくして当たり判定も消す（でもオブジェクトはActiveのまま）
        if (sr) sr.enabled = false;
        if (col) col.enabled = false;

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        // 復活
        if (sr) sr.enabled = true;
        if (col) col.enabled = true;

        isRespawning = false;
    }
}
