//using UnityEngine;
//using System.Collections;

//public class Player_Stop_Controller_s : MonoBehaviour
//{
//    private CameraController cameraController;

//    void Start()
//    {
//        // シーン内の CameraController を探して取得
//        cameraController = FindObjectOfType<CameraController>();
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        // 相手が wall タグなら反応
//        if (collision.gameObject.CompareTag("wall"))
//        {
//            StartCoroutine(HandleWallCollision());
//        }
//    }

//    private IEnumerator HandleWallCollision()
//    {
//        Debug.Log("wall に衝突！5秒待機...");
//        yield return new WaitForSeconds(5f);

//        if (cameraController != null)
//        {
//            Debug.Log("カメラ停止（0.5秒間）");
//            cameraController.isPaused = true;

//            yield return new WaitForSeconds(0.5f);

//            cameraController.isPaused = false;
//            Debug.Log("カメラ再開！");
//        }
//    }
//}
using UnityEngine;
using System.Collections;

// --------------------------------------------------------------
// Player_Stop_Controller_s
// 壁に衝突 → 5秒待機 → 最新の到達X座標へ「滑らかに移動」
// 強制右スクロールで置いて行かれるのを防止するためのスクリプト。
// --------------------------------------------------------------
public class Player_Stop_Controller_s : MonoBehaviour
{
    private CameraController cameraController;

    // プレイヤーが右方向に移動していった最大X座標
    private float maxX;

    // プレイヤーが maxX に戻る際の「移動速度」
    // Inspector から調整可能にするため public にしている
    public float returnSpeed = 5f;

    void Start()
    {
        maxX = transform.position.x; // 初期位置を最初の最大Xにする
        cameraController = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        // 右に進んだときだけ maxX を更新する
        if (transform.position.x > maxX)
        {
            maxX = transform.position.x;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            StartCoroutine(HandleWallCollision());
        }
    }

    // --------------------------------------------------------------
    // 壁衝突後の一連の動作をコルーチンで処理
    // --------------------------------------------------------------
    private IEnumerator HandleWallCollision()
    {
        Debug.Log("wall に衝突！5秒待機中...");
        yield return new WaitForSeconds(5f); // ▼ 5 秒待機

        // ▼ スムーズに maxX まで移動する処理を開始
        Debug.Log("最新到達Xまで滑らかに移動開始");

        yield return StartCoroutine(SmoothMoveToMaxX());

        Debug.Log("移動完了！");

        // ----------------------------------------------------------
        // ▼ カメラ停止処理（必要なら）
        // ----------------------------------------------------------
        if (cameraController != null)
        {
            Debug.Log("カメラ停止（0.5秒間）");
            cameraController.isPaused = true;

            yield return new WaitForSeconds(0.5f);

            cameraController.isPaused = false;
            Debug.Log("カメラ再開！");
        }
        else
        {
            Debug.LogWarning("CameraController が見つかりません。カメラ停止処理スキップ。");
        }
    }

    // --------------------------------------------------------------
    // ■ プレイヤーを maxX の位置へ「滑らかに移動」させる処理
    // --------------------------------------------------------------
    private IEnumerator SmoothMoveToMaxX()
    {
        // 動きが暴走しないように小さな許容範囲
        float threshold = 0.01f;

        // maxX に到達するまでループ
        while (Mathf.Abs(transform.position.x - maxX) > threshold)
        {
            // ▼ Lerp（線形補間）で少しずつ maxX へ近づける
            float newX = Mathf.Lerp(transform.position.x, maxX, Time.deltaTime * returnSpeed);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            // 1フレーム待つ
            yield return null;
        }

        // 最後に誤差を無くしてピッタリ maxX に合わせる
        transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
    }
}