using System.Collections;
using UnityEngine;

public class TreeDemoAutoJump_t : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpForce = 5f;

    [Header("参照")]
    public PlayerScrole_t playerMove;          // ★プレイヤーのオート移動
    public CameraController_t cameraCtrl;      // ★カメラのオートスクロール

    [Header("リセット位置")]
    public Transform resetPlayerPos;           // ★スタート位置（空オブジェクト）
    public Transform resetCameraPos;           // ★カメラ初期位置（空オブジェクト）

    [Header("ループ待ち")]
    public float loopWaitMin = 1f;
    public float loopWaitMax = 2f;

    Rigidbody2D rb;
    bool resetting = false;

    public Transform treeDemoRoot;   // ★TreeDemoRoot（木がぶら下がってる親）
    public AutoClickTreeDemo_t treeDemoAuto;  // ★AutoClickTreeDemo_tを入れる


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            StartCoroutine(ResetLoop());
        }
    }

    //IEnumerator ResetLoop()
    //{
    //    // ① まず動きを止める
    //    if (playerMove != null) playerMove.enabled = false;
    //    if (cameraCtrl != null) cameraCtrl.isPaused = true;

    //    // ★デモで生成した木を全削除
    //    if (treeDemoRoot != null)
    //    {
    //        for (int i = treeDemoRoot.childCount - 1; i >= 0; i--)
    //        {
    //            Destroy(treeDemoRoot.GetChild(i).gameObject);
    //        }
    //    }


    //    // ② 速度を止める
    //    rb.linearVelocity = Vector2.zero;
    //    rb.angularVelocity = 0f;

    //    // ③ 少し待つ（1〜2秒）
    //    float wait = Random.Range(loopWaitMin, loopWaitMax);
    //    yield return new WaitForSeconds(wait);

    //    // ④ 位置を戻す（Zも安全に固定）
    //    if (resetPlayerPos != null)
    //    {
    //        transform.position = resetPlayerPos.position;
    //    }

    //    if (resetCameraPos != null)
    //    {
    //        Vector3 p = resetCameraPos.position;
    //        p.z = -10f; // ★2Dカメラはだいたい -10
    //        Camera.main.transform.position = p;
    //    }

    //    // ⑤ 1フレーム待つ（めり込み防止）
    //    yield return null;

    //    // ⑥ 再開
    //    if (playerMove != null) playerMove.enabled = true;
    //    if (cameraCtrl != null) cameraCtrl.isPaused = false;

    //    resetting = false;

    //    // ★周回開始＝木を1秒後に出す
    //    if (treeDemoAuto != null)
    //    {
    //        treeDemoAuto.StartCycle();
    //    }

    //}
    IEnumerator ResetLoop()
    {
        // ① まず動きを止める（スクリプト/カメラ）
        if (playerMove != null) playerMove.enabled = false;
        if (cameraCtrl != null) cameraCtrl.isPaused = true;

        // ★デモで生成した木を全削除
            if (treeDemoRoot != null)
            {
                for (int i = treeDemoRoot.childCount - 1; i >= 0; i--)
                {
                    Destroy(treeDemoRoot.GetChild(i).gameObject);
                }
            }

        // ② 物理を完全停止（ガクガク防止の本命）
        rb.simulated = false;

        // ③ 待機（1〜2秒）
        float wait = Random.Range(loopWaitMin, loopWaitMax);
        yield return new WaitForSeconds(wait);

        // ④ 位置をリセット（物理OFF中なので安定）
        if (resetPlayerPos != null)
            transform.position = resetPlayerPos.position;

        if (resetCameraPos != null)
        {
            Vector3 p = resetCameraPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }

        // ⑤ 速度をゼロ（念のため）
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // ⑥ 1フレーム待つ（接地判定・衝突解決が安定する）
        yield return null;

        // ⑦ 物理再開
        rb.simulated = true;

        // ⑧ さらに 1フレーム待つ（復帰直後の震え防止）
        yield return null;

        // ⑨ 再開
        if (playerMove != null) playerMove.enabled = true;
        if (cameraCtrl != null) cameraCtrl.isPaused = false;

        resetting = false;

    // ★周回開始＝木を1秒後に出す
        if (treeDemoAuto != null)
        {
            treeDemoAuto.StartCycle();
        }
    }


}


