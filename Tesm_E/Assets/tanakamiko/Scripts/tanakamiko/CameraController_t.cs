using UnityEngine;

public class CameraController_t : MonoBehaviour
{
    public float scrollSpeed = 3.0f;     // カメラXスクロール速度
    public Transform player;             // プレイヤー
    public static string gameState = "playing";  // "playing" / "clear" / "gameover"
    public bool isPaused = false;

    [Header("Y追従（木/土デモ用）")]
    public bool followY = false;         // ★木/土パネルだけONにする
    public float yOffset = 0f;           // ★プレイヤーより少し上を映したいなら + に
    public float ySmooth = 8f;           // ★大きいほど追従が速い（6～12おすすめ）
    public float minY = -999f;           // ★下限（必要なら）
    public float maxY = 999f;           // ★上限（必要なら）

    void Start()
    {
        Application.targetFrameRate = 60;
        gameState = "playing";
    }

    void FixedUpdate()
    {
        if (gameState != "playing") return;
        if (isPaused) return;

        // playerが消えたらゲームオーバー（元の仕様）
        if (player == null)
        {
            gameState = "gameover";
            return;
        }

        // ① Xスクロール（今まで通り）
        Vector3 pos = transform.position;
        pos.x += scrollSpeed * Time.fixedDeltaTime;

        // ② Y追従（必要なときだけ）
        if (followY)
        {
            float targetY = player.position.y + yOffset;

            // 上下制限
            targetY = Mathf.Clamp(targetY, minY, maxY);

            // なめらか追従
            pos.y = Mathf.Lerp(pos.y, targetY, ySmooth * Time.fixedDeltaTime);
        }

        transform.position = pos;
    }
    //public float scrollSpeed = 3.0f;  // カメラのスクロール速度
    //public Transform player;          // プレイヤー
    //public static string gameState = "playing";  // "playing" / "clear" / "gameover"
    //public bool isPaused = false;     // ← カメラ一時停止用

    //void Start()
    //{
    //    Application.targetFrameRate = 60;
    //    gameState = "playing";
    //}

    //void FixedUpdate()
    //{
    //    // ゲーム中でなければ何もしない
    //    if (gameState != "playing") return;

    //    // 一時停止中はスクロールしない
    //    if (isPaused) return;

    //    // プレイヤーが消えたらゲームオーバー
    //    if (player == null)
    //    {
    //        gameState = "gameover";
    //        return;
    //    }

    //    // カメラを右にスクロール
    //    transform.position += new Vector3(scrollSpeed * Time.fixedDeltaTime, 0, 0);
    //}
}
