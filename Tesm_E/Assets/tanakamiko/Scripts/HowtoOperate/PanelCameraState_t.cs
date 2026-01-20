using UnityEngine;

public class PanelCameraState_t : MonoBehaviour
{
    public CameraController_t cameraCtrl;
    public Transform cameraStartPos;     // CameraStartPos

    [Header("このパネル中にカメラを動かす？")]
    public bool playCamera = false;

    void OnEnable()
    {
        // カメラ位置リセット
        if (cameraStartPos != null)
        {
            Vector3 p = cameraStartPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }

        // 動かす/止める
        if (cameraCtrl != null)
            cameraCtrl.isPaused = !playCamera;
    }

    void OnDisable()
    {
        // パネルを出たら止める（事故防止）
        if (cameraCtrl != null)
            cameraCtrl.isPaused = true;
    }
}
