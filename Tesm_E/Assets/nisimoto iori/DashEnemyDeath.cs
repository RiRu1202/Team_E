using UnityEngine;

public class DashEnemyDeath : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;   // 左に移動するスピード

    private bool isDead = false;   // 敵が倒されたかどうかのフラグ

    void Start()
    {
        // Rigidbody2D を取得
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 重力を無効化して空中に浮かせる（落ちないように）
            rb.gravityScale = 0;
        }
    }

    void Update()
    {
        // 倒されていないときだけ移動処理を行う
        if (!isDead)
        {
            Move();
        }
    }

    void Move()
    {
        // 常に左方向へ一定速度で移動する
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーの攻撃に当たったら削除
        if (!isDead && other.CompareTag("PlayerAttack"))
        {
            isDead = true;          // 死亡フラグを立てる
            Destroy(gameObject);    // 自身を削除
        }
    }

    void OnBecameInvisible()
    {
        // 画面外に出たら自動で削除（メモリ節約）
        Destroy(gameObject);
    }
}