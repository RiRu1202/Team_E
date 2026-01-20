using System.Collections;
using UnityEngine;

public class SoilDemoAutoJump_t : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpForce = 5f;

    [Header("参照")]
    public PlayerScrole_t playerMove;
    public CameraController_t cameraCtrl;
    public AutoClickSoilDemo_t soilAuto;     // ★ループ後に土生成を再スタート
    public Transform soilDemoRoot;           // ★土を消す親（SoilDemoRoot）

    [Header("リセット位置")]
    public Transform resetPlayerPos;
    public Transform resetCameraPos;

    [Header("ループ待ち")]
    public float loopWaitMin = 1f;
    public float loopWaitMax = 2f;

    Rigidbody2D rb;
    bool resetting = false;

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

    IEnumerator ResetLoop()
    {
        // 止める
        if (playerMove != null) playerMove.enabled = false;
        if (cameraCtrl != null) cameraCtrl.isPaused = true;

        // 物理停止（ガクガク防止）
        rb.simulated = false;

        // ★生成した土を全削除（次周回の邪魔防止）
        if (soilDemoRoot != null)
        {
            for (int i = soilDemoRoot.childCount - 1; i >= 0; i--)
                Destroy(soilDemoRoot.GetChild(i).gameObject);
        }

        // 待機
        float wait = Random.Range(loopWaitMin, loopWaitMax);
        yield return new WaitForSeconds(wait);

        // 位置戻し
        if (resetPlayerPos != null)
            transform.position = resetPlayerPos.position;

        if (resetCameraPos != null)
        {
            Vector3 p = resetCameraPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        yield return null;
        rb.simulated = true;
        yield return null;

        // 再開
        if (playerMove != null) playerMove.enabled = true;
        if (cameraCtrl != null) cameraCtrl.isPaused = false;

        // ★周回開始：走り出して1秒後に土を出す
        if (soilAuto != null) soilAuto.StartCycle();

        resetting = false;
    }
}

