using UnityEngine;
using System.Collections;

public class HitodamaRespawn_t : MonoBehaviour
{
    public float respawnDelay = 2.0f;

    SpriteRenderer sr;
    Collider2D col;

    bool isRespawning = false;
    Coroutine routine;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Die()
    {
        if (isRespawning) return;

        routine = StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        // 消えたようにする（でもGameObjectはOFFにしない）
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        ForceReset(); // 復活
    }

    // ★これが今回のキモ：いつでも正常状態に戻せる
    public void ForceReset()
    {
        // コルーチンが途中なら止める
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        isRespawning = false;

        if (sr != null)
        {
            sr.enabled = true;
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }

        if (col != null) col.enabled = true;
    }
    
}
