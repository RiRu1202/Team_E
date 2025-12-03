using UnityEngine;

public class FlyEnemyDeath : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;           // 左右の移動スピード
    public float moveDistance = 3f;    // 左右の移動範囲

    [Header("ふわふわ設定")]
    public float floatSpeed = 2f;      // 上下に動くスピード（波の速さ）
    public float floatHeight = 0.5f;   // 上下に動く高さ（波の大きさ）

    private Vector2 startPos;          // 初期位置（基準点）
    private bool movingRight = true;   // 現在右に動いているかどうか
    private bool isDead = false;       // 敵が倒されたかどうかのフラグ

    void Start()
    {
        // 初期位置を保存（上下ふわふわの基準になる）
        startPos = transform.position;

        // Rigidbody2D（物理演算）を取得
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 重力を無効化して空中に浮かせる
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
        // 左右移動
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x > startPos.x + moveDistance)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < startPos.x - moveDistance)
                movingRight = true;
        }

        // 上下ふわふわ移動（サイン波で上下する）
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // 現在のX位置を保ちながらY位置だけ変更
        transform.position = new Vector2(transform.position.x, newY);
    }
}