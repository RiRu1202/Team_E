using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockController_p : MonoBehaviour
{
    public GameObject rockPrefab;
    public Vector2 checkBoxSize = new Vector2(1f, 1f);
    public LayerMask blockLayer;
    public float shiftDistance = 1.0f;
    public string playerGateName = "PlayerGate";
    public float spawnOffset = 3f;

    [Header("クールタイム設定")]
    public float cooldownTime = 1.5f;
    private bool isOnCooldown = false;

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
        // ★追加
        if (cardTransform != null)
            cardTransform.localScale = normalScale;
    }

    void Update()
    {
        // ★追加：クールタイム表示
        if (cardRenderer != null)
        {
            cardRenderer.color =
                isOnCooldown ? cooldownColor : readyColor;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedTarget())
            {
                if (isOnCooldown)
                    return;

                GameObject gate = GameObject.Find(playerGateName);
                if (gate != null)
                {
                    Vector2 spawnPos =
                        (Vector2)gate.transform.position + Vector2.right * spawnOffset;

                    TrySpawnRock(spawnPos);

                    // ★追加：押し込み演出
                    StartCoroutine(PressEffect());

                    StartCoroutine(StartCooldown());
                }
            }
        }
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

    private bool IsClickedTarget()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        return hit.collider != null && hit.collider.CompareTag("Rock_Frag");
    }

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
