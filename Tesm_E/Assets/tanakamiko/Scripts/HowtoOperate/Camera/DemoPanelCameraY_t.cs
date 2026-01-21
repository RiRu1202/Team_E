using UnityEngine;

public class DemoPanelCameraY_t : MonoBehaviour
{
    public CameraController_t cameraCtrl;
    public bool enableFollowY = true;   // Tree/Soil‚Ítrue

    void OnEnable()
    {
        if (cameraCtrl == null) return;
        cameraCtrl.followY = enableFollowY;
    }

    void OnDisable()
    {
        if (cameraCtrl == null) return;
        cameraCtrl.followY = false;    // ƒpƒlƒ‹ŠO‚Å‚ÍOFF‚É–ß‚·
    }
}

