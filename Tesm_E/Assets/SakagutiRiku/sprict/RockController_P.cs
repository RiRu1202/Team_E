using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;  // ← 必須！！
using System.Collections.Generic;

public class RockController : MonoBehaviour
{
    public GameObject rockPrefab;
    public Vector2 checkBoxSize = new Vector2(1f, 1f);
    public LayerMask blockLayer;
    public float shiftDistance = 1.0f;
    public string targetObjectName = "RockButton";
    public string playerGateName = "PlayerGate";
    public float spawnOffset = 3f;

    [Header("クールタイム設定")]
    public float cooldownTime = 1.5f;  // クールタイム秒
    private bool isOnCooldown = false; // クールタイム中？

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedTarget(targetObjectName))
            {
                // クールタイム中なら生成しない
                if (isOnCooldown)
                {
                    Debug.Log("クールタイム中！岩は生成されません。");
                    return;
                }

                GameObject gate = GameObject.Find(playerGateName);
                if (gate != null)
                {
                    Vector2 spawnPos = (Vector2)gate.transform.position + Vector2.right * spawnOffset;
                    TrySpawnRock(spawnPos);

                    // クールタイム開始
                    StartCoroutine(StartCooldown());
                }
                else
                {
                    Debug.LogError("PlayerGateが見つかりません！");
                }
            }
        }
    }

    // クールタイム開始
    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
        Debug.Log("クールタイム終了！");
    }

    // クリックしたオブジェクトが targetName かどうか判定
    private bool IsClickedTarget(string targetName)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult r in results)
        {
            if (r.gameObject.name == targetName)
                return true;
        }

        return false;
    }

    // 指定位置から岩を生成、ブロックがあれば左にずらす
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
                Debug.Log($"岩を {i} 回左にずらして生成しました。位置: {pos}");
                return;
            }

            pos += Vector2.left * shiftDistance;
        }

        Debug.LogWarning("空いている場所が見つかりませんでした。岩を生成しません。");
    }

    // Sceneビューで判定範囲を見えるようにする
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        GameObject gate = GameObject.Find(playerGateName);
        if (gate != null)
            Gizmos.DrawWireCube((Vector2)gate.transform.position + Vector2.right * spawnOffset, checkBoxSize);
    }
}
