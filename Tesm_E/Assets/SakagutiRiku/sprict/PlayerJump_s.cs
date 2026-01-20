using UnityEngine;

/// <summary>
/// 2Dプレイヤーの自動移動・ジャンプを制御するスクリプト
/// ・右方向へオートスクロール
/// ・地面にいる時のみジャンプ可能
/// ・壁の横を地面と誤判定しない
/// </summary>
public class PlayerJump_s : MonoBehaviour
{
    // =============================
    // プレイヤーの移動設定
    // =============================
    [Header("プレイヤーの移動設定")]
    public float moveSpeed = 5f;   // 右方向への移動速度（オートスクロール）
    public float jumpForce = 5f;   // ジャンプ時の上方向速度

    // Rigidbody2D 参照
    private Rigidbody2D rb;

    // 状態管理用フラグ
    private bool isGrounded = false;  // 地面に接しているかどうか
    private bool isJumping = false;   // ジャンプ中かどうか

    void Start()
    {
        // フレームレートを60fpsに固定（動作の安定化）
        Application.targetFrameRate = 60;

        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // =============================
        // ★ オートスクロール移動処理 ★
        // =============================

        // ジャンプ中は少し移動速度を落とす
        float xSpeed = isJumping ? moveSpeed * 0.6f : moveSpeed;

        // X方向の速度を常に一定に保つ
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);

        // =============================
        // ★ ジャンプ処理 ★
        // =============================
        // ・スペースキーを押した
        // ・地面に接している
        // 上記2条件を満たしたときだけジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;  // ジャンプ状態にする（空中判定）

            // 落下中のY速度を一度リセット（ジャンプを安定させる）
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            // 上方向へ一定速度を与えてジャンプ
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // =============================
        // ★ 着地判定 ★
        // =============================
        // 地面に接していればジャンプ状態を解除
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    // =============================
    // ★ 地面判定処理 ★
    // =============================
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 接触したオブジェクトが Ground レイヤーの場合のみ判定
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // 接触点をすべてチェック
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 接触面の法線が上向きなら「上から乗った」と判断
                // normal.y > 0.1f にすることで壁の横を誤判定しない
                if (contact.normal.y > 0.1f)
                {
                    isGrounded = true; // 地面に接地
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Ground レイヤーから離れたら空中判定にする
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
