using System.Collections;
using UnityEngine;

/// <summary>
/// Tree_Frag をクリックしたら木を生成する
/// Canvasなし（ワールド配置の札）対応版
/// ・最初の1回のみクールタイムなし
/// ・押し込み演出（Scale変更）
/// ・クールタイム中は暗く表示
/// </summary>
public class TreeController_t : MonoBehaviour
{
    [Header("木生成設定")]
    public GameObject treePrefab;
    public float cooldown = 0.5f;
    public float spawnDistance = 5f;

    [Header("成長設定")]
    public float growHeight = 3.5f;
    public float growTime = 0.5f;
    public float treeWidth = 2f;

    [Header("プレイヤー設定")]
    public Transform playerGate;
    public LayerMask groundLayer;

    [Header("札（Canvasなし）設定")]
    public string targetTag = "Tree_Frag";
    public Transform cardTransform;          // ★ Transform
    public SpriteRenderer cardRenderer;      // ★ 見た目

    [Header("札サイズ設定")]
    public Vector3 normalScale = new Vector3(0.8f, 0.8f, 1f);
    public Vector3 pressedScale = new Vector3(0.7f, 0.7f, 1f);
    public float pressDuration = 0.1f;

    [Header("色設定")]
    public Color readyColor = Color.white;
    public Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    [Header("デモ用：Goal基準で木を出す")]
    public Transform demoGoal;          // ★DemoGoalを入れる
    public float goalBackDistance = 6f; // ★6マス前


    private float passedTime = 0f;
    private bool firstUse = true;
    private bool isPressing = false;

    public Transform demoParent;   // ★デモで生成した木を入れる親（TreeDemoRoot）


    void Start()
    {
        passedTime = cooldown;

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

        if (!IsClickedTreeFrag()) return;
        if (isPressing) return;

        if (firstUse)
        {
            StartCoroutine(PressEffect());
            SpawnTreeNow();
            firstUse = false;
            passedTime = 0f;
            return;
        }

        if (passedTime >= cooldown)
        {
            StartCoroutine(PressEffect());
            SpawnTreeNow();
            passedTime = 0f;
        }
    }

    //==============================
    // クリック判定（2D Raycast）
    //==============================
    private bool IsClickedTreeFrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        return hit.collider != null && hit.collider.CompareTag(targetTag);
    }

    //==============================
    // 押し込み演出
    //==============================
    private IEnumerator PressEffect()
    {
        isPressing = true;

        cardTransform.localScale = pressedScale;
        yield return new WaitForSeconds(pressDuration * 0.5f);
        cardTransform.localScale = normalScale;

        isPressing = false;
    }

    //==============================
    // クールタイム中の色変更
    //==============================
    private void UpdateCardColor()
    {
        if (cardRenderer == null) return;

        if (firstUse)
        {
            cardRenderer.color = readyColor;
            return;
        }

        cardRenderer.color =
            passedTime >= cooldown ? readyColor : cooldownColor;
    }

    //==============================
    // 木生成処理
    //==============================
    private void SpawnTreeNow()
    {
        if (playerGate == null) return;

        float spawnX;

        if (demoGoal != null)
        {
            // ★DemoGoalの6マス前に固定
            spawnX = demoGoal.position.x - goalBackDistance;
        }
        else
        {
            // 従来方式（保険）
            float dir = playerGate.localScale.x > 0 ? 1 : -1;
            spawnX = playerGate.position.x + (spawnDistance * dir);
        }

        // Ray開始位置（上から下へ地面探す）
        Vector3 rayStart = new Vector3(spawnX, playerGate.position.y + 5f, 0f);


        RaycastHit2D hit =
            Physics2D.Raycast(rayStart, Vector2.down, 10f, groundLayer);

        if (hit.collider != null)
        {
            Transform parent = (demoParent != null) ? demoParent : null;
            GameObject tree = Instantiate(treePrefab, hit.point, Quaternion.identity, parent);

            //GameObject tree =
            //    Instantiate(treePrefab, hit.point, Quaternion.identity, demoParent);

            StartCoroutine(GrowTree(tree.transform));
        }
        else
        {
            Debug.LogWarning("木を生成する地面が見つかりません。");
        }
    }

    //==============================
    // 木の成長アニメーション
    //==============================
    private IEnumerator GrowTree(Transform tree)
    {
        Vector3 startScale = new Vector3(treeWidth, 0.1f, 1f);
        Vector3 endScale = new Vector3(treeWidth, growHeight, 1f);

        float time = 0f;
        tree.localScale = startScale;

        while (time < growTime)
        {
            tree.localScale =
                Vector3.Lerp(startScale, endScale, time / growTime);
            time += Time.deltaTime;
            yield return null;
        }

        tree.localScale = endScale;
    }

    //==============================
    // ★ デモ用：外部から木生成を呼ぶ
    //==============================
    public void DemoSpawnTree()
    {
        // 押し込み演出やクールタイム処理は
        // 外側（自動クリック側）でやるので
        // ここは生成だけ
        SpawnTreeNow();
    }

}

