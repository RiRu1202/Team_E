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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            StartCoroutine(HandleWallCollision());
        }
    }

    private IEnumerator HandleWallCollision()
    {
        Debug.Log("壁にぶつかった！2秒待機...");
        yield return new WaitForSeconds(2f);

        if (cameraController != null)
        {
            Debug.Log("カメラ停止（1秒間）");
            cameraController.isPaused = true;
            yield return new WaitForSeconds(1f);
            cameraController.isPaused = false;
            Debug.Log("カメラ再開！");
        }
    }
}
