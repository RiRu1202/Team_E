using UnityEngine;

public class CameraController_n : MonoBehaviour
{
    public float scrollSpeed = 3.0f;  // カメラのスクロール速度
    public Transform player;          // プレイヤー
    public Transform goal;            // ゴール
    public static string gameState = "playing";  // "playing" / "clear" / "gameover"
    public bool isPaused = false;     // ← カメラ一時停止用

    void Start()
    {
        Application.targetFrameRate = 60;
        gameState = "playing";
    }

    void FixedUpdate()
    {
        // ゲーム中でなければ何もしない
        if (gameState != "playing") return;

        // 一時停止中はスクロールしない
        if (isPaused) return;

        // プレイヤーが消えたらゲームオーバー
        if (player == null)
        {
            gameState = "gameover";
            return;
        }

        // プレイヤーがゴールに到達したらクリア
        if (player.position.x >= goal.position.x)
        {
            gameState = "clear";
            return;
        }

        // カメラを右にスクロール
        transform.position += new Vector3(scrollSpeed * Time.fixedDeltaTime, 0, 0);
    }
}
