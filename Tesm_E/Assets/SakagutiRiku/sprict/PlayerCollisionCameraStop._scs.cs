using UnityEngine;
using System.Collections;

public class PlayerCollisionCameraStop_s : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;                 // 停止させたいカメラ（参照をInspectorから設定）
    [SerializeField] private MonoBehaviour cameraControllerScript; // カメラ動作を制御しているスクリプト（参照）
    [SerializeField] private float waitBeforeFreeze = 3f;         // 衝突後、停止開始までの待ち時間（秒）
    [SerializeField] private float freezeDuration = 1f;            // 停止させる時間（秒）

    private bool isTriggered = false;                              // 一度トリガーされたかどうか

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("wall"))
        {
            isTriggered = true;
            StartCoroutine(FreezeCameraAfterDelay());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("wall"))
        {
            isTriggered = true;
            StartCoroutine(FreezeCameraAfterDelay());
        }
    }

    private IEnumerator FreezeCameraAfterDelay()
    {
        // 待機
        yield return new WaitForSeconds(waitBeforeFreeze);

        Debug.Log("カメラ停止開始");

        if (cameraControllerScript != null)
        {
            cameraControllerScript.enabled = false; // カメラ動作停止
        }
        else
        {
            Debug.LogWarning("cameraControllerScript が設定されていません。");
        }

        // 停止時間
        yield return new WaitForSeconds(freezeDuration);

        if (cameraControllerScript != null)
        {
            cameraControllerScript.enabled = true;  // カメラ再開
        }

        Debug.Log("カメラ再開");
        isTriggered = false; // 必要なら、再びトリガー可能に
    }
}
