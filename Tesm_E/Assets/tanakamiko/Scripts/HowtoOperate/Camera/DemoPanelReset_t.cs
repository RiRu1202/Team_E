using UnityEngine;

public class DemoPanelReset_t : MonoBehaviour
{
    [Header("止めたいもの（あれば入れる）")]
    public CameraController_t cameraCtrl;
    public Transform cameraStartPos;

    public MonoBehaviour playerMove;      // PlayerScrole_t を入れる
    public Rigidbody2D playerRb;          // Player_DemoのRigidbody2D
    public Transform playerStartPos;

    [Header("デモで生成したものを消す親（木/土のSpawnedRoot）")]
    public Transform spawnedRoot;

    [Header("自動札（StartCycleがある方）")]
    public AutoClickTreeDemo_t treeAuto;
    public AutoClickSoilDemo_t soilAuto;

    void OnDisable()
    {
        // ① カメラ停止＋位置戻し
        if (cameraCtrl != null) cameraCtrl.isPaused = true;
        if (cameraStartPos != null)
        {
            Vector3 p = cameraStartPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }

        // ② プレイヤー停止＋位置戻し
        if (playerMove != null) playerMove.enabled = false;

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;
        }

        if (playerStartPos != null)
        {
            if (playerRb != null) playerRb.simulated = false;
            playerRb.transform.position = playerStartPos.position;
            if (playerRb != null) playerRb.simulated = true;
        }

        // ③ 生成物を全削除（木/土）
        if (spawnedRoot != null)
        {
            for (int i = spawnedRoot.childCount - 1; i >= 0; i--)
                Destroy(spawnedRoot.GetChild(i).gameObject);
        }
    }

    void OnEnable()
    {
        // パネルを開いた瞬間に動かしたいならここで再開
        if (playerMove != null) playerMove.enabled = true;
        if (cameraCtrl != null) cameraCtrl.isPaused = false;

        // 木/土は「走り出して1秒後に出す」なら、ここでStartCycle
        if (treeAuto != null) treeAuto.StartCycle();
        if (soilAuto != null) soilAuto.StartCycle();
    }
}
