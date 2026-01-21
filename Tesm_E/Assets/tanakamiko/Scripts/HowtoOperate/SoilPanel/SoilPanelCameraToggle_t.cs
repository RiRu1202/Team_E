using UnityEngine;

public class SoilPanelCameraToggle_t : MonoBehaviour
{
    public CameraController_t cameraCtrl;

    void OnEnable()
    {
        if (cameraCtrl != null) cameraCtrl.isPaused = false;
    }

    void OnDisable()
    {
        if (cameraCtrl != null) cameraCtrl.isPaused = true;
    }
}
