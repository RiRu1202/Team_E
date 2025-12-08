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
    public string targetUITagName = "Fire_Frag";  // ← クリック対象のタグ

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
        // マウス位置をワールド座標に変換
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2D用のレイキャスト（Z方向ではなくXY平面）
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (objPrefab == null) return;

        passedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.CompareTag("Fire_Frag"))
            {
                if (passedTime >= delayTime)
                {
                    Fire();
                    passedTime = 0f;
                }
            }
        }
    }

    /// <summary>
    /// クリックされたUIの中に tag が一致するものがあるか判定
    /// </summary>
    private bool IsClickedUIByTag(string targetTag)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            // ★タグで判定するように変更↓
            if (result.gameObject.CompareTag(targetTag))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Playergate から弾を発射
    /// </summary>
    private void Fire()
    {
        if (gateTransform == null) return;

        Vector2 pos = gateTransform.position;

        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            Vector2 dir = gateTransform.right;
            rbody.AddForce(dir * fireSpeed, ForceMode2D.Impulse);
        }
    }
}
