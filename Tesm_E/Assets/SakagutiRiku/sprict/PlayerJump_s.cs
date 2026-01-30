using UnityEngine;

// <summary>
// 2D プレイヤーの移動とジャンプを制御するスクリプト（オートスクロール）
// 壁に引っかかりにくいよう調整済み
// </summary>
public class PlayerJump_s : MonoBehaviour
{
    [Header("プレイヤーの移動設定")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isJumping = false;

    // 接地が一瞬途切れてもジャンプできるようにするため
    private float coyoteTime = 0.1f;      // 地面を離れてから許容する時間
    private float coyoteTimer = 0f;

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
        // ★ 接地（コヨーテタイム処理） ★
        // -----------------------------
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;  // 地面に触れている間リセット
        }
        else
        {
            coyoteTimer -= Time.deltaTime; // 時間経過で減少
        }

        // -----------------------------
        // ★ ジャンプ処理 ★
        // -----------------------------
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimer > 0f)
        {
            isJumping = true;
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 着地したら空中フラグ解除
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    // -----------------------------
    // ★ 地面判定（groundCheck なし版）★
    // -----------------------------
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            JudgeGround(collision);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            JudgeGround(collision);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // 完全に地面から離れた → isGrounded を false にするが
            // すぐにはジャンプ不能にならない（coyoteTime のおかげ）
            isGrounded = false;
        }
    }

    /// <summary>
    /// 接触方向から地面かどうかを判定（壁の横判定を無視）
    /// </summary>
    void JudgeGround(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            // ● normal.y > 0.1 → 足元からの接触
            // ● contact.point.y < プレイヤーの中心より下 → 正しく地面
            if (normal.y > 0.1f && contact.point.y < transform.position.y - 0.05f)
            {
                isGrounded = true;
                return;
            }
        }
    }
}
