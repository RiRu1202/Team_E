using System.Collections;
using UnityEngine;

public class TreeDemoAutoJump_t : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpForce = 5f;

    [Header("参照")]
    public PlayerScrole_t playerMove;
    public CameraController_t cameraCtrl;
    public AutoClickTreeDemo_t treeAuto;     // 走り出して1秒後に木
    public Transform treeSpawnedRoot;        // 生成した木だけが入る親（消してOKな方）

    [Header("リセット位置")]
    public Transform resetPlayerPos;
    public Transform resetCameraPos;

    [Header("ループ待ち")]
    public float loopWaitMin = 1f;
    public float loopWaitMax = 2f;

    Rigidbody2D rb;
    bool resetting = false;
    Coroutine resetRoutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // ★パネルに戻ってきた時に“途中状態”をリセット
        resetting = false;

        if (rb != null) rb.simulated = true;
        if (playerMove != null) playerMove.enabled = true;
        if (cameraCtrl != null) cameraCtrl.isPaused = false;

        // 木を最初から始めたいなら（任意）
        if (treeAuto != null) treeAuto.StartCycle();
    }

    void OnDisable()
    {
        // ★途中で次へ/戻るを押された時の“後始末”
        if (resetRoutine != null)
        {
            StopCoroutine(resetRoutine);
            resetRoutine = null;
        }

        resetting = false;

        if (rb != null)
        {
            rb.simulated = true;          // OFFのまま残さない
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (playerMove != null) playerMove.enabled = true;  // OFFのまま残さない
        if (cameraCtrl != null) cameraCtrl.isPaused = true; // パネル外では止める

        // 木が邪魔なら消す（任意）
        if (treeSpawnedRoot != null)
        {
            for (int i = treeSpawnedRoot.childCount - 1; i >= 0; i--)
                Destroy(treeSpawnedRoot.GetChild(i).gameObject);
        }
    }

    void DoJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DemoJump"))
        {
            DoJump();
        }

        if (other.CompareTag("DemoGoal") && !resetting)
        {
            resetting = true;
            resetRoutine = StartCoroutine(ResetLoop());
        }
    }

    IEnumerator ResetLoop()
    {
        // ①止める
        if (playerMove != null) playerMove.enabled = false;
        if (cameraCtrl != null) cameraCtrl.isPaused = true;
        if (rb != null) rb.simulated = false;

        // ②木を消す（任意）
        if (treeSpawnedRoot != null)
        {
            for (int i = treeSpawnedRoot.childCount - 1; i >= 0; i--)
                Destroy(treeSpawnedRoot.GetChild(i).gameObject);
        }

        // ③待つ
        float wait = Random.Range(loopWaitMin, loopWaitMax);
        yield return new WaitForSeconds(wait);

        // ④位置戻し
        if (resetPlayerPos != null) transform.position = resetPlayerPos.position;

        if (resetCameraPos != null)
        {
            Vector3 p = resetCameraPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }

        // ⑤復帰
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = true;
        }

        yield return null;

        if (playerMove != null) playerMove.enabled = true;
        if (cameraCtrl != null) cameraCtrl.isPaused = false;

        // ★周回開始：木をまた1秒後に出す
        if (treeAuto != null) treeAuto.StartCycle();

        resetting = false;
        resetRoutine = null;
    }
    //[Header("ジャンプ設定")]
    //public float jumpForce = 5f;

    //[Header("参照")]
    //public PlayerScrole_t playerMove;          // ★プレイヤーのオート移動
    //public CameraController_t cameraCtrl;      // ★カメラのオートスクロール

    //[Header("リセット位置")]
    //public Transform resetPlayerPos;           // ★スタート位置（空オブジェクト）
    //public Transform resetCameraPos;           // ★カメラ初期位置（空オブジェクト）

    //[Header("ループ待ち")]
    //public float loopWaitMin = 1f;
    //public float loopWaitMax = 2f;

    //Rigidbody2D rb;
    //bool resetting = false;

    //public Transform treeDemoRoot;   // ★TreeDemoRoot（木がぶら下がってる親）
    //public AutoClickTreeDemo_t treeDemoAuto;  // ★AutoClickTreeDemo_tを入れる


    //void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //}

    //void DoJump()
    //{
    //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("DemoJump"))
    //    {
    //        DoJump();
    //    }

    //    if (other.CompareTag("DemoGoal") && !resetting)
    //    {
    //        resetting = true;
    //        StartCoroutine(ResetLoop());
    //    }
    //}
    //IEnumerator ResetLoop()
    //{
    //    // ① まず動きを止める（スクリプト/カメラ）
    //    if (playerMove != null) playerMove.enabled = false;
    //    if (cameraCtrl != null) cameraCtrl.isPaused = true;

    //    // ★デモで生成した木を全削除
    //        if (treeDemoRoot != null)
    //        {
    //            for (int i = treeDemoRoot.childCount - 1; i >= 0; i--)
    //            {
    //                Destroy(treeDemoRoot.GetChild(i).gameObject);
    //            }
    //        }

    //    // ② 物理を完全停止（ガクガク防止の本命）
    //    rb.simulated = false;

    //    // ③ 待機（1〜2秒）
    //    float wait = Random.Range(loopWaitMin, loopWaitMax);
    //    yield return new WaitForSeconds(wait);

    //    // ④ 位置をリセット（物理OFF中なので安定）
    //    if (resetPlayerPos != null)
    //        transform.position = resetPlayerPos.position;

    //    if (resetCameraPos != null)
    //    {
    //        Vector3 p = resetCameraPos.position;
    //        p.z = -10f;
    //        Camera.main.transform.position = p;
    //    }

    //    // ⑤ 速度をゼロ（念のため）
    //    rb.linearVelocity = Vector2.zero;
    //    rb.angularVelocity = 0f;

    //    // ⑥ 1フレーム待つ（接地判定・衝突解決が安定する）
    //    yield return null;

    //    // ⑦ 物理再開
    //    rb.simulated = true;

    //    // ⑧ さらに 1フレーム待つ（復帰直後の震え防止）
    //    yield return null;

    //    // ⑨ 再開
    //    if (playerMove != null) playerMove.enabled = true;
    //    if (cameraCtrl != null) cameraCtrl.isPaused = false;

    //    resetting = false;

    //// ★周回開始＝木を1秒後に出す
    //    if (treeDemoAuto != null)
    //    {
    //        treeDemoAuto.StartCycle();
    //    }
    //}
}


