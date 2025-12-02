using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// マウスクリックで特定のUIボタン（例：Fire_Frag）が押されたとき、
/// プレイヤーの「発射口（playergate）」から弾（objPrefab）を発射するスクリプト。
/// 最初の1発はすぐに発射可能で、その後はクールタイム(delayTime)が発生する。
/// </summary>
public class FireController_n : MonoBehaviour
{
    [Header("発射設定")]
    public GameObject objPrefab;   // 発射する弾
    public float delayTime = 1f;   // 発射間隔
    public float fireSpeed = 4f;   // 弾の速度

    [Header("発射対象タグ")]
    public string fireTag = "Fire_tag";  // ← クリック対象のタグ

    private Transform gateTransform;     // 発射位置（playergate）
    private float passedTime = 0f;

    void Start()
    {
        // Playergate を取得（このスクリプトをPlayerに付ける前提）
        gateTransform = transform.Find("playergate");

        passedTime = delayTime;
    }

    void Update()
    {
        passedTime += Time.deltaTime;

        // 左クリック
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("iti");
            // Fire_tag をクリックしたか？
            if (CheckClickedObjectHasTag2D(fireTag))
            {
                Debug.Log("当たり");
                if (passedTime >= delayTime)
                {
                    Fire();
                    passedTime = 0f;
                }
            }
        }
    }

    /// <summary>
    /// クリックした2Dオブジェクトにtagが付いているか判定
    /// </summary>
    private bool CheckClickedObjectHasTag2D(string tagName)
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // クリック地点の2D Raycast
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.CompareTag(tagName);
        }

        return false;
    }

    /// <summary>
    /// Playergate から弾を発射
    /// </summary>
    private void Fire()
    {
        if (gateTransform == null)
        {
            Debug.LogWarning("playergate が見つかりません！");
            return;
        }

        // 発射位置は Playergate の場所！
        Vector2 pos = gateTransform.position;
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Playergate の右方向へ飛ばす
            Vector2 dir = gateTransform.right;

            rb.AddForce(dir * fireSpeed, ForceMode2D.Impulse);
        }
    }
}
