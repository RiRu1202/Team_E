using UnityEngine;
using System.Collections;

public class PlayerCollisionCameraStop_n : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private MonoBehaviour cameraControllerScript;
    [SerializeField] private float waitBeforeFreeze = 3f;
    [SerializeField] private float freezeDuration = 1f;

    private bool isTriggered = false;

// 横方向の厳密な判定に使う角度（±10°以内なら左側衝突）
[SerializeField] private float leftHitAngleRange = 10f;

private void OnCollisionEnter2D(Collision2D collision)
{
    if (!collision.gameObject.CompareTag("wall")) return;

    foreach (var contact in collision.contacts)
    {
        Vector2 normal = contact.normal;

        // normal が (-1, 0) を向いている角度を計算
        float angle = Vector2.Angle(normal, Vector2.left); // 左向きとの差

        // ほぼ完全に左方向からぶつかった（角度 ±10°）
        if (angle <= leftHitAngleRange)
        {
            if (!isTriggered)
            {
                isTriggered = true;
                StartCoroutine(FreezeCameraAfterDelay());
            }
            return;
        }
    }
}

private IEnumerator FreezeCameraAfterDelay()
{
    yield return new WaitForSeconds(waitBeforeFreeze);

    if (cameraControllerScript != null)
        cameraControllerScript.enabled = false;

    yield return new WaitForSeconds(freezeDuration);

    if (cameraControllerScript != null)
        cameraControllerScript.enabled = true;

    isTriggered = false;
}
}
