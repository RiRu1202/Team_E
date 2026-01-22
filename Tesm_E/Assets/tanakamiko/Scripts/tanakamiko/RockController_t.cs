using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockController_t : MonoBehaviour
{
    [Header("岩生成設定")]
    public GameObject rockPrefab;
    public GameObject previewRock;
    public Vector2 checkBoxSize = new Vector2(1f, 1f);
    public LayerMask blockLayer;
    public float shiftDistance = 1.0f;
    public string playerGateName = "PlayerGate";
    public float spawnOffset = 3f;

    [Header("クールタイム設定")]
    public float cooldownTime = 1.5f;
    private bool isOnCooldown = false;

    [Header("札（Canvasなし）演出用")]
    public Transform cardTransform;
    public SpriteRenderer cardRenderer;

    public Vector3 normalScale = new Vector3(0.8f, 0.8f, 1f);
    public Vector3 pressedScale = new Vector3(0.7f, 0.7f, 1f);
    public float pressDuration = 0.1f;

    public Color readyColor = Color.white;
    public Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private bool isPressing = false;

    void Start()
    {
        if (cardTransform != null)
            cardTransform.localScale = normalScale;

        if (previewRock != null)
            previewRock.SetActive(false);
    }

    void Update()
    {
        // クールタイム中の札の色変更
        if (cardRenderer != null)
        {
            cardRenderer.color = isOnCooldown ? cooldownColor : readyColor;
        }

        // =============================
        // 生成位置プレビュー表示
        // =============================
        // ★ クールタイム中は表示しない ★
        if (!isOnCooldown && IsMouseOnRockFrag())
        {
            GameObject gate = GameObject.Find(playerGateName);
            if (gate != null)
            {
                Vector2 startPos =
                    (Vector2)gate.transform.position + Vector2.right * spawnOffset;

                Vector2 previewPos;
                if (TryGetSpawnPosition(startPos, out previewPos))
                {
                    previewRock.SetActive(true);
                    previewRock.transform.position = previewPos;
                }
                else
                {
                    previewRock.SetActive(false);
                }
            }
        }
        else
        {
            previewRock.SetActive(false);
        }

        // =============================
        // クリックで岩生成
        // =============================
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOnRockFrag() && !isOnCooldown)
            {
                GameObject gate = GameObject.Find(playerGateName);
                if (gate != null)
                {
                    Vector2 startPos =
                        (Vector2)gate.transform.position + Vector2.right * spawnOffset;

                    TrySpawnRock(startPos);
                    StartCoroutine(PressEffect());
                    StartCoroutine(StartCooldown());
                }
            }
        }
    }

    // =============================
    // クールタイム処理
    // =============================
    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;

        // 念のため即非表示
        if (previewRock != null)
            previewRock.SetActive(false);

        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

    // =============================
    // マウスが Rock_Frag 上か判定
    // =============================
    private bool IsMouseOnRockFrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        return hit.collider != null && hit.collider.CompareTag("Rock_Frag");
    }

    // =============================
    // 岩を生成する
    // =============================
    public void TrySpawnRock(Vector2 startPos)
    {
        Vector2 pos = startPos;
        int maxTries = 5;

        for (int i = 0; i < maxTries; i++)
        {
            Collider2D hit = Physics2D.OverlapBox(pos, checkBoxSize, 0f, blockLayer);
            if (hit == null)
            {
                Instantiate(rockPrefab, pos, Quaternion.identity);
                return;
            }

            pos += Vector2.left * shiftDistance;
        }
    }

    // =============================
    // 生成可能位置を取得（プレビュー用）
    // =============================
    private bool TryGetSpawnPosition(Vector2 startPos, out Vector2 result)
    {
        Vector2 pos = startPos;
        int maxTries = 5;

        for (int i = 0; i < maxTries; i++)
        {
            Collider2D hit = Physics2D.OverlapBox(pos, checkBoxSize, 0f, blockLayer);
            if (hit == null)
            {
                result = pos;
                return true;
            }

            pos += Vector2.left * shiftDistance;
        }

        result = Vector2.zero;
        return false;
    }

    // =============================
    // 札の押下演出
    // =============================
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
