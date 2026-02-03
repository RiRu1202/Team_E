using UnityEngine;

/// <summary>
/// 2D プレイヤーの移動とジャンプを制御するスクリプト（オートスクロール）
/// 壁に引っかかりにくく、空中で壁に当たっても着地しない完全版
/// </summary>
public class PlayerJump_s : MonoBehaviour
{
    [Header("プレイヤーの移動設定")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isJumping = false;

    // コヨーテタイム
    private float coyoteTime = 0.1f;
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
        // ★ コヨーテタイム更新 ★
        // -----------------------------
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        // -----------------------------
        // ★ ジャンプ処理 ★
        // -----------------------------
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimer > 0f)
        {
            isJumping = true;
            isGrounded = false;

            // Y速度を一度リセットして安定ジャンプ
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            coyoteTimer = 0f;
        }

        // 着地したらジャンプ状態解除
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    // -----------------------------
    // ★ 地面判定 ★
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
            isGrounded = false;
        }
    }

    /// <summary>
    /// 接触方向と位置から「地面のみ」を判定
    /// 壁・段差・斜め接触を除外
    /// </summary>
    void JudgeGround(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            // 条件まとめ：
            // ・ほぼ真上方向の法線
            // ・接触点がプレイヤー中心より下
            // ・落下中 or ほぼ停止中
            if (normal.y > 0.7f &&
                contact.point.y < transform.position.y - 0.05f &&
                rb.linearVelocity.y <= 0.1f)
            {
                isGrounded = true;
                return;
            }
        }
    }
}
