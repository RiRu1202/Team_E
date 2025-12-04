using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockController_t : MonoBehaviour
{
    public GameObject rockPrefab;
    public Vector2 checkBoxSize = new Vector2(1f, 1f);
    public LayerMask blockLayer;
    public float shiftDistance = 1.0f;
    public string playerGateName = "PlayerGate";
    public float spawnOffset = 3f;

    [Header("クールタイム設定")]
    public float cooldownTime = 1.5f;  // クールタイム秒
    private bool isOnCooldown = false; // クールタイム中？

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Rock_Frag をクリックしたか？
            if (IsClickedTarget())
            {
                // クールタイム中は生成しない
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

    // クリックしたオブジェクトが "Rock_Frag" タグかどうかだけを判定
    private bool IsClickedTarget()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null)
        {
            Debug.Log("Raycast が何にも当たっていません");
            return false;
        }

        Debug.Log("Raycast が当たったオブジェクト: " + hit.collider.gameObject.name +
                  " / タグ: " + hit.collider.gameObject.tag);

        if (hit.collider.CompareTag("Rock_Frag"))
        {
            Debug.Log("Rock_Frag をクリックしました！");
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
