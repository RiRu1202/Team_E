using UnityEngine;

/// <summary>
/// 2D プレイヤーの移動とジャンプを制御するスクリプト
/// </summary>
public class PlayerScrole_t : MonoBehaviour
{
    [Header("プレイヤーの移動設定")]
    public float moveSpeed = 5f;   // 横移動の速さ
    public float jumpForce = 5f;   // ジャンプ力（上方向への力）

    private Rigidbody2D rb;        // プレイヤーの Rigidbody2D コンポーネント（物理演算に使用）
    private bool isGrounded = false; // 地面に接しているかどうか（ジャンプ制御に使用）

    // ゲーム開始時に一度だけ呼ばれるメソッド
    void Start()
    {
        // フレームレートを60FPSに固定
        Application.targetFrameRate = 60;

        // Rigidbody2D コンポーネントを取得してキャッシュ
        rb = GetComponent<Rigidbody2D>();
    }

    // 毎フレーム呼ばれるメソッド
    void Update()
    {
        // スペースキーが押され、かつ地面に接している場合のみジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 上方向に力を加えてジャンプ
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // 何かと衝突した瞬間に呼ばれる（2D 物理）
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトが「Ground」レイヤーだった場合、地面に接地していると判定
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    // 衝突していたオブジェクトから離れた瞬間に呼ばれる（2D 物理）
    void OnCollisionExit2D(Collision2D collision)
    {
        // 離れたオブジェクトが「Ground」レイヤーだった場合、接地していないと判定
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
