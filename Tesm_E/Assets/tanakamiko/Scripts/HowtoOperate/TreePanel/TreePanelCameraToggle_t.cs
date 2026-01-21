using UnityEngine;

public class TreePanelCameraToggle_t : MonoBehaviour
{
    public CameraController_t cameraCtrl;


    void OnEnable()
    {
        if (cameraCtrl != null)
            cameraCtrl.isPaused = false;   // TreePanel’†‚¾‚¯“®‚©‚·
    }

    void OnDisable()
    {
        if (cameraCtrl != null)
            cameraCtrl.isPaused = true;    // ‚»‚êˆÈŠO‚ÍŽ~‚ß‚é
    }
}
