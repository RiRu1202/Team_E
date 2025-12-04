using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Water_Controller_t : MonoBehaviour
{
    [Header("発射設定")]
    public GameObject objPrefab;
    public float delayTime = 1f;
    public float fireSpeed = 4.0f;

    // 子オブジェクト "playergate"
    private Transform gateTransform;
    private float passedTime = 0f;

    void Start()
    {
        gateTransform = transform.Find("playergate");
        passedTime = delayTime;
    }

    void Update()
    {
        if (objPrefab == null) return;

        passedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            // タグ Water_Frag をクリックしたとき
            if (IsClickedWaterFrag())
            {
                if (passedTime >= delayTime)
                {
                    Water();
                    passedTime = 0f;
                }
            }
        }
    }

    /// <summary>
    /// クリック対象が Water_Frag タグを持っているか判定（UI or 2D どちらでもOK）
    /// </summary>
    private bool IsClickedWaterFrag()
    {
        // ▼ UI（Canvas上のUIボタンなど）のタグチェック
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Water_Frag"))
            {
                return true;
            }
        }

        // ▼ 2D物理 Raycast（シーン上の2Dオブジェクト用）
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Water_Frag"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 弾を発射
    /// </summary>
    public void Water()
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
