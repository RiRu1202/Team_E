using UnityEngine;

/// <summary>
/// オートスクロール型2Dゲーム用のカメラ制御スクリプト
/// ・右方向へ自動スクロール
/// ・Goalタグに触れたらスクロール停止
/// ・一時停止対応
/// </summary>
public class CameraController : MonoBehaviour
{
    // =============================
    // カメラ設定
    // =============================
    public float scrollSpeed = 3.0f;  // カメラが右へ移動する速度

    // =============================
    // ゲーム状態管理
    // =============================
    public static string gameState = "playing";

    // カメラの一時停止フラグ
    public bool isPaused = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        gameState = "playing";
    }

    void FixedUpdate()
    {
        // プレイ中でなければ動かさない
        if (gameState != "playing") return;

        // 停止中ならスクロールしない
        if (isPaused) return;

        // カメラを右へオートスクロール
        transform.position += new Vector3(
            scrollSpeed * Time.fixedDeltaTime,
            0,
            0
        );
    }

    // =============================
    // ★ ゴール接触判定 ★
    // =============================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 触れた相手が Goal タグなら
        if (collision.CompareTag("Goal"))
        {
            // カメラを停止
            isPaused = true;

            // 必要なら状態変更（任意）
            gameState = "clear";
        }
    }
}
