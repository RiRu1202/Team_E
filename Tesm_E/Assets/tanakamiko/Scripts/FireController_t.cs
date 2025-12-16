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

    [Header("札UI設定")]
    public string targetUITagName = "Fire_Frag";
    public Transform cardTransform;        // 札のTransform
    public SpriteRenderer cardRenderer;    // 暗くする用

    [Header("押し込み演出（数値）")]
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 pressedScale = new Vector3(0.4f, 0.47f, 1f);
    public float pressDuration = 0.1f;

    [Header("色設定")]
    public Color readyColor = Color.white;
    public Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private Transform gateTransform;
    private float passedTime = 0f;
    private bool isPressAnimationPlaying = false;

    void Start()
    {
        gateTransform = transform.Find("playergate");
        passedTime = delayTime;

        if (cardTransform != null)
            cardTransform.localScale = normalScale;

        if (cardRenderer != null)
            cardRenderer.color = readyColor;
    }

    void Update()
    {
        passedTime += Time.deltaTime;
        UpdateCardColor();

        if (!Input.GetMouseButtonDown(0)) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // ▼ 押せない条件
        if (hit.collider == null) return;
        if (!hit.collider.CompareTag(targetUITagName)) return;
        if (passedTime < delayTime) return;
        if (isPressAnimationPlaying) return;

        // ▼ 押された演出（スクリプト）
        StartCoroutine(PressEffect());

        // ▼ 炎発射
        Fire();
        passedTime = 0f;
    }

    IEnumerator PressEffect()
    {
        isPressAnimationPlaying = true;

        // 縮む
        cardTransform.localScale = pressedScale;

        yield return new WaitForSeconds(pressDuration * 0.5f);

        // 戻る
        cardTransform.localScale = normalScale;

        isPressAnimationPlaying = false;
    }

    void UpdateCardColor()
    {
        if (cardRenderer == null) return;

        if (passedTime >= delayTime)
            cardRenderer.color = readyColor;
        else
            cardRenderer.color = cooldownColor;
    }

    void Fire()
    {
        if (gateTransform == null || objPrefab == null) return;

        Vector2 pos = gateTransform.position;
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 dir = gateTransform.right;
            rb.AddForce(dir * fireSpeed, ForceMode2D.Impulse);
        }
    }
    //[Header("発射設定")]
    //public GameObject objPrefab;      // 炎プレハブ
    //public float delayTime = 1f;      // クールタイム
    //public float fireSpeed = 4.0f;    // 発射速度

    //[Header("札UI設定")]
    //public string targetUITagName = "Fire_Frag"; // 札のタグ
    //public Animator cardAnimator;                 // 札のAnimator
    //public string pressTriggerName = "Press";     // 押されたTrigger名

    //private Transform gateTransform;
    //private float passedTime = 0f;

    //void Start()
    //{
    //    gateTransform = transform.Find("playergate");
    //    passedTime = delayTime;
    //}

    //void Update()
    //{
    //    passedTime += Time.deltaTime;

    //    if (!Input.GetMouseButtonDown(0)) return;

    //    // マウス位置 → ワールド座標
    //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    // UIを2D Raycastで取得
    //    RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

    //    if (hit.collider == null) return;
    //    if (!hit.collider.CompareTag(targetUITagName)) return;
    //    if (passedTime < delayTime) return;

    //    // ★札の押されたアニメーション
    //    if (cardAnimator != null)
    //    {
    //        cardAnimator.SetTrigger(pressTriggerName);
    //    }

    //    // ★炎発射
    //    Fire();

    //    passedTime = 0f;
    //}

    //void Fire()
    //{
    //    if (gateTransform == null || objPrefab == null) return;

    //    Vector2 pos = gateTransform.position;
    //    GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

    //    Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
    //    if (rb != null)
    //    {
    //        Vector2 dir = gateTransform.right;
    //        rb.AddForce(dir * fireSpeed, ForceMode2D.Impulse);
    //    }
    //}
}
