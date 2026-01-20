using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要

public class PlayerController_s : MonoBehaviour
{
    Rigidbody2D rbody;               // Rigidbody2D型の変数
    public float speed = 3.0f;

    public static string gameState = "playing";  // ゲームの状態

    void Start()
    {
        Application.targetFrameRate = 60;

        // Rigidbody2Dを取得
        rbody = GetComponent<Rigidbody2D>();
        gameState = "playing";  // ゲーム中に設定
    }

    void Update()
    {
        if (gameState != "playing") return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトが「Dead」レイヤーまたは「Enemy」レイヤーだった場合、GameOverにする判定
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dead"))
        {
            Debug.Log("ゲームオーバー画面移動");
            GameOver();
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing") return;

        // Rigidbody2Dの速度を更新
        rbody.linearVelocity = new Vector2(speed, rbody.linearVelocity.y);
    }

    // ゲームオーバー処理
    void GameOver()
    {
        Debug.Log("ゲームオーバー！");
        SceneManager.LoadScene("GameOva"); // シーン名を正しく修正
    }

    // ゴール判定
    /*void OnTriggerEnter2D(Collider2D other)
    {
        // 触れた相手が "Goal" タグの場合のみ反応
        if (other.CompareTag("Goal"))
        {
            // カメラ停止などに使うゲーム状態を変更
            CameraController.gameState = "clear";

            // "Clear" シーンに切り替え
            SceneManager.LoadScene("Clear");
        }
    }*/
}
