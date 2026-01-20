using UnityEngine;
public class DashEnemyDeath_t : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;

    private bool isDead = false;
    private Rigidbody2D rb;
    private Collider2D col;
    private int moveDir = -1; // -1:左、1:右

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.gravityScale = 1;

        // Enemy タグ同士の衝突を無視
        IgnoreEnemyCollisions();
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    void Move()
    {
        transform.Translate(Vector2.right * moveDir * speed * Time.deltaTime);
    }

    // ■ 壁に当たったら反転
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            Flip();
        }
    }

    // ■ 方向と見た目を反転
    void Flip()
    {
        moveDir *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ★ Enemy タグ同士の衝突を無視
    void IgnoreEnemyCollisions()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy == this.gameObject) continue;

            Collider2D otherCol = enemy.GetComponent<Collider2D>();
            if (otherCol != null && col != null)
            {
                Physics2D.IgnoreCollision(col, otherCol, true);
            }
        }
    }
}