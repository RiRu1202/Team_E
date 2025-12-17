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

    // ===== ここから追加 =====
    [Header("札（Canvasなし）演出用")]
    public Transform cardTransform;
    public SpriteRenderer cardRenderer;

    public Vector3 normalScale = new Vector3(0.8f, 0.8f, 1f);
    public Vector3 pressedScale = new Vector3(0.7f, 0.7f, 1f);
    public float pressDuration = 0.1f;

    public Color readyColor = Color.white;
    public Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private bool isPressing = false;
    // ===== 追加ここまで =====

    void Start()
    {
        gateTransform = transform.Find("playergate");
        passedTime = delayTime;

        // ★追加
        if (cardTransform != null)
            cardTransform.localScale = normalScale;
    }

    void Update()
    {
        if (objPrefab == null) return;

        passedTime += Time.deltaTime;

        // ★追加：クールタイム表示
        if (cardRenderer != null)
        {
            cardRenderer.color =
                passedTime >= delayTime ? readyColor : cooldownColor;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // タグ Water_Frag をクリックしたとき
            if (IsClickedWaterFrag())
            {
                if (passedTime >= delayTime)
                {
                    Water();

                    // ★追加：押し込み演出
                    StartCoroutine(PressEffect());

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
        // UI判定
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Water_Frag"))
                return true;
        }

        // 2D物理 Raycast
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        return hit.collider != null && hit.collider.CompareTag("Water_Frag");
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

    // ★追加：押し込み演出
    IEnumerator PressEffect()
    {
        if (isPressing) yield break;
        isPressing = true;

        cardTransform.localScale = pressedScale;
        yield return new WaitForSeconds(pressDuration * 0.5f);
        cardTransform.localScale = normalScale;

        isPressing = false;
    }
}
