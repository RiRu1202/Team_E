using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireController_t : MonoBehaviour
{
    [Header("発射設定")]
    public GameObject objPrefab;
    public float delayTime = 1f;
    public float fireSpeed = 4.0f;

    [Header("UI設定（タグ）")]
    public string targetUITagName = "Fire_Frag";  // ← UIのタグで判定するように変更

    private Transform gateTransform;
    private float passedTime = 0f;

    void Start()
    {
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

    public void Fire()
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
