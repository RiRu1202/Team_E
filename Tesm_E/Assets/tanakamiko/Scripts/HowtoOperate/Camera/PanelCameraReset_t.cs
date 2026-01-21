using UnityEngine;

public class PanelCameraReset_t : MonoBehaviour
{
    public CameraController_t cameraCtrl;
    public Transform cameraStartPos;   // CameraStartPos（空オブジェクト）

    void OnEnable()
    {
        if (cameraCtrl != null)
        {
            cameraCtrl.isPaused = false;
        }

        if (cameraStartPos != null)
        {
            Vector3 p = cameraStartPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }
    }

    void OnDisable()
    {
        if (cameraCtrl != null)
        {
            cameraCtrl.isPaused = true;
        }
    }
}
