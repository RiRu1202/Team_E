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

    // ★ いつでも初期状態に戻すための関数
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
    //public float respawnDelay = 2.0f;

    //bool isRespawning = false;

    //SpriteRenderer sr;
    //Collider2D col;

    //void Awake()
    //{
    //    sr = GetComponent<SpriteRenderer>();
    //    col = GetComponent<Collider2D>();
    //}

    //public void Die()
    //{
    //    if (isRespawning) return;
    //    isRespawning = true;

    //    // 見えなくして当たり判定も消す（でもオブジェクトはActiveのまま）
    //    if (sr) sr.enabled = false;
    //    if (col) col.enabled = false;

    //    StartCoroutine(Respawn());
    //}

    //IEnumerator Respawn()
    //{
    //    yield return new WaitForSeconds(respawnDelay);

    //    // 復活
    //    if (sr) sr.enabled = true;
    //    if (col) col.enabled = true;

    //    isRespawning = false;
    //}
}
