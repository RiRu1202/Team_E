using UnityEngine;
using System.Collections;

public class Player_Stop_Controller_n : MonoBehaviour
{
    private CameraController cameraController;

    // 左方向判定の許容角度（厳密にしたいなら 5～10°）
    [SerializeField] private float leftHitAngleRange = 10f;

    void Start()
    {
        // シーン内の CameraController を探して取得
        cameraController = FindObjectOfType<CameraController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // wall タグ以外は無視
        if (!collision.gameObject.CompareTag("wall")) return;

        // すべての接触点をチェックして左方向からの衝突を判定
        foreach (var contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            // 左方向（Vector2.left）との差の角度
            float angle = Vector2.Angle(normal, Vector2.left);

            // 十分に左方向からの衝突ならカメラ停止処理開始
            if (angle <= leftHitAngleRange)
            {
                StartCoroutine(HandleWallCollision());
                return;
            }
        }
    }

    private IEnumerator HandleWallCollision()
    {
        Debug.Log("左から wall に衝突！5秒待機...");
        yield return new WaitForSeconds(5f);

        if (cameraController != null)
        {
            Debug.Log("カメラ停止（0.3秒間）");
            cameraController.isPaused = true;

            yield return new WaitForSeconds(0.3f);

            cameraController.isPaused = false;
            Debug.Log("カメラ再開！");
        }
    }
}
