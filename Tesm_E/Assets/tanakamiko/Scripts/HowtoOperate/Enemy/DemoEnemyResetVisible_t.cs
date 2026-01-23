using UnityEngine;

public class DemoEnemyResetVisible_t : MonoBehaviour
{
    SpriteRenderer sr;
    Collider2D col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // パネルを再表示した時（GameObjectがONになった時）に必ず復帰
    void OnEnable()
    {
        ForceVisible();
        StopAllCoroutines(); // 途中の復活待ちが残ってても止める
    }

    public void ForceVisible()
    {
        if (sr != null)
        {
            sr.enabled = true;

            // 「透明化（colorのalpha=0）」系の対策
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }

        if (col != null) col.enabled = true;
    }
}
