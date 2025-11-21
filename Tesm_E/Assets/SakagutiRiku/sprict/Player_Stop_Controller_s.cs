using UnityEngine;
using System.Collections;

public class Player_Stop_Controller_s : MonoBehaviour
{
    private CameraController cameraController;

    void Start()
    {
        // シーン内の CameraController を探して取得
        cameraController = FindObjectOfType<CameraController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 相手が wall タグなら反応
        if (collision.gameObject.CompareTag("wall"))
        {
            StartCoroutine(HandleWallCollision());
        }
    }

    private IEnumerator HandleWallCollision()
    {
        Debug.Log("wall に衝突！5秒待機...");
        yield return new WaitForSeconds(5f);

        if (cameraController != null)
        {
            Debug.Log("カメラ停止（0.5秒間）");
            cameraController.isPaused = true;

            yield return new WaitForSeconds(0.5f);

            cameraController.isPaused = false;
            Debug.Log("カメラ再開！");
        }
    }
}
