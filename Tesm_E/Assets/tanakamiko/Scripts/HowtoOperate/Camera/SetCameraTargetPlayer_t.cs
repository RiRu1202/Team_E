using UnityEngine;

public class SetCameraTargetPlayer_t : MonoBehaviour
{
    public CameraController_t cameraCtrl;
    public Transform targetPlayer;   // このパネルの主人公
    public bool enableFollowY = true;

    void OnEnable()
    {
        if (cameraCtrl == null) return;
        if (targetPlayer != null) cameraCtrl.player = targetPlayer; // ★ここ重要
        cameraCtrl.followY = enableFollowY;
    }

    void OnDisable()
    {
        if (cameraCtrl == null) return;
        cameraCtrl.followY = false;
    }
}
