using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Tree_Frag タグクリックで木を生成し、成長アニメーション付き
/// 最初の1回だけクールタイムなし
/// Groundレイヤーの地面上に生成
/// </summary>
public class TreeController_t : MonoBehaviour
{
    [Header("木生成設定")]
    public GameObject treePrefab;      // 生成する木のプレハブ
    public float cooldown = 0.5f;      // クールタイム
    public float spawnDistance = 5f;   // playerGate から5マス先

    [Header("成長設定")]
    public float growHeight = 3.5f;    // 木の最終高さ
    public float growTime = 0.5f;      // 成長にかかる時間
    public float treeWidth = 2f;       // 木の横幅

    [Header("プレイヤー設定")]
    public Transform playerGate;       // 木を出す位置
    public LayerMask groundLayer;      // 地面レイヤー

    private bool canSpawn = true;      // クールタイム中かどうか
    private bool firstUse = true;      // 最初の1回だけクールタイム無し

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedTreeFrag())
            {
                TrySpawnTree();
            }
        }
    }

    // Tree_Frag タグがクリックされたか判定
    private bool IsClickedTreeFrag()
    {
        // UI判定
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult r in results)
        {
            if (r.gameObject.CompareTag("Tree_Frag"))
                return true;
        }

        // 2Dオブジェクト判定
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Tree_Frag"))
            return true;

        return false;
    }

    private void TrySpawnTree()
    {
        if (!canSpawn || playerGate == null) return;
        StartCoroutine(SpawnTree());
    }

    private IEnumerator SpawnTree()
    {
        canSpawn = false;

        // playerGate の向き
        float dir = playerGate.localScale.x > 0 ? 1 : -1;

        // playerGate から spawnDistance 先に上から Ray を飛ばして地面判定
        Vector3 rayStart = playerGate.position + new Vector3(spawnDistance * dir, 5f, 0f); // 上方向から
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 10f, groundLayer);

        if (hit.collider != null)
        {
            // 地面に当たった位置に木を生成
            GameObject tree = Instantiate(treePrefab, hit.point, Quaternion.identity);

            // 成長アニメーション開始
            StartCoroutine(GrowTree(tree.transform));
        }
        else
        {
            Debug.LogWarning("木を生成する地面が見つかりません。");
        }

        // 最初の1回だけクールタイムなし
        if (firstUse)
        {
            firstUse = false;
            canSpawn = true;
            yield break;
        }

        // 通常クールタイム
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }

    // 成長アニメーション
    private IEnumerator GrowTree(Transform tree)
    {
        Vector3 startScale = new Vector3(treeWidth, 0.1f, 1);
        Vector3 endScale = new Vector3(treeWidth, growHeight, 1);
        float time = 0f;
        tree.localScale = startScale;

        while (time < growTime)
        {
            tree.localScale = Vector3.Lerp(startScale, endScale, time / growTime);
            time += Time.deltaTime;
            yield return null;
        }

        tree.localScale = endScale;
    }
}
