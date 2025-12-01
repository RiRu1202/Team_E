using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraChaseGameOver_t: MonoBehaviour
{
    public Transform cam; // メインカメラ
    public float offset = -2f; // プレイヤーがカメラより何m後ろに行ったらアウトにするか
    void Update()
    {
        // プレイヤーのX位置 < カメラのX位置 + offset ならゲームオーバー
        if (transform.position.x < cam.position.x + offset)
        {
            SceneManager.LoadScene("GameOva");
        }
    }
}