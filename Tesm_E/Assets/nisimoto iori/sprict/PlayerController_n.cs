using UnityEngine;

public class PlayerController_n:MonoBehaviour
{
    [Header("プレイヤーの移動設定")]
    public float moveSpeed = 5f;   // オートスクロール速度
    public float jumpForce = 5f;   // ジャンプ力

    private Rigidbody2D rb;

    private bool isGrounded = false;  // 地面に接しているか
    private bool isJumping = false;   // ジャンプ中かどうか

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // -----------------------------
        // ★ オートスクロール移動 ★
        // -----------------------------
        float xSpeed = isJumping ? moveSpeed * 0.6f : moveSpeed;
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);

        // -----------------------------
        // ★ ジャンプ処理（AddForce禁止 → velocity制御）★
        // -----------------------------
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;           // 空中状態にする（誤判定防止）
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);  // 落下中の勢いリセット
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // 安定したジャンプ
        }

        // 着地したらジャンプ状態解除
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    // -----------------------------
    // ★ 地面判定（壁の横を誤判定しない）★
    // -----------------------------
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // normal.y > 0.5 ＝ 上から乗った時だけ接地と判定
                if (contact.normal.y > 0.1f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
