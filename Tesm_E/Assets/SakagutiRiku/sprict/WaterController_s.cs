using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// マウスクリックで特定のUIボタン（例：Fire_Frag）が押されたとき、
/// プレイヤーの「発射口（playergate）」から弾（objPrefab）を発射するスクリプト。
/// 最初の1発はすぐに発射可能で、その後はクールタイム(delayTime)が発生する。
/// </summary>
public class Water_Controller_s : MonoBehaviour
{
    [Header("発射設定")]
    public GameObject objPrefab;         // 発射するオブジェクト（例：弾、火球など）のプレハブ
    public float delayTime = 1f;         // 発射間隔（秒）
    public float fireSpeed = 4.0f;       // 弾を飛ばす速度（力の大きさ）

    [Header("UI設定")]
    public string targetUIButtonName = "Water_Frag"; // 発射をトリガーするUIボタンの名前

    private Transform gateTransform;     // 弾の発射位置（子オブジェクト "playergate" のTransform）
    private float passedTime = 0f;       // 前回の発射から経過した時間

    // ゲーム開始時に1度だけ呼ばれる
    void Start()
    {
        // 子オブジェクト "playergate" を探してキャッシュ
        gateTransform = transform.Find("playergate");

        // 最初の一発をすぐに撃てるように、経過時間をdelayTimeで初期化
        passedTime = delayTime;
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // 発射するオブジェクト（プレハブ）が設定されていない場合は何もしない
        if (objPrefab == null) return;

        // 経過時間を加算（発射間隔の管理に使用）
        passedTime += Time.deltaTime;

        // マウスの左クリックが押された瞬間
        if (Input.GetMouseButtonDown(0))
        {
            // 指定したUI（targetUIButtonName）がクリックされたかチェック
            if (IsClickedUI(targetUIButtonName))
            {
                // 一定の発射間隔（delayTime）が経過していたら発射
                if (passedTime >= delayTime)
                {
                    Fire();          // 弾を発射
                    passedTime = 0f; // 経過時間をリセット
                }
            }
        }
    }

    /// <summary>
    /// 指定したUI要素がクリックされたかどうかを判定するメソッド
    /// </summary>
    private bool IsClickedUI(string targetUIName)
    {
        // 現在のマウス位置からUI上のクリック判定を作成
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        // クリック位置にある全てのUI要素を取得
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // クリックされたUIの中に指定の名前があるかチェック
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == targetUIName)
            {
                return true; // 指定したUIがクリックされていた
            }
        }

        return false; // 該当UIがクリックされていない
    }

    /// <summary>
    /// 弾（objPrefab）を生成して、前方向（playergate の右方向）に発射する
    /// </summary>
    public void Fire()
    {
        // 発射位置が設定されていない場合は何もしない
        if (gateTransform == null) return;

        // 弾の生成位置を取得（playergate の位置）
        Vector2 pos = gateTransform.position;

        // 弾プレハブを生成
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

        // 弾に Rigidbody2D がついていれば力を加えて発射
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            // playergate の「右方向」に向かって発射
            Vector2 dir = gateTransform.right;
            rbody.AddForce(dir * fireSpeed, ForceMode2D.Impulse);
        }
    }
}
