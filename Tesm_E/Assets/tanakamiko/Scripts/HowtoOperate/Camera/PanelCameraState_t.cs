using UnityEngine;
using System.Collections;

public class PanelCameraState_t : MonoBehaviour
{
    public CameraController_t cameraCtrl;
    public Transform cameraStartPos;

    [Header("このパネルでカメラを動かす")]
    public bool playCamera = false;

    [Header("Y追従（木・土だけON）")]
    public bool followY = false;

    [Header("Y追従を有効にするまでの遅延")]
    public float followYDelay = 0.2f;

    Coroutine followRoutine;

    void OnEnable()
    {
        // ① カメラ位置を即リセット
        if (cameraStartPos != null)
        {
            Vector3 p = cameraStartPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }

        if (cameraCtrl != null)
        {
            cameraCtrl.isPaused = !playCamera;

            // ★ いったん必ずOFF
            cameraCtrl.followY = false;

            // ★ 遅延してON
            if (followY)
                followRoutine = StartCoroutine(EnableFollowYAfterDelay());
        }
    }

    void OnDisable()
    {
        if (followRoutine != null)
        {
            StopCoroutine(followRoutine);
            followRoutine = null;
        }

        if (cameraCtrl != null)
        {
            cameraCtrl.isPaused = true;
            cameraCtrl.followY = false;
        }
    }

    IEnumerator EnableFollowYAfterDelay()
    {
        yield return new WaitForSeconds(followYDelay);

        if (cameraCtrl != null)
            cameraCtrl.followY = true;
    }
}
