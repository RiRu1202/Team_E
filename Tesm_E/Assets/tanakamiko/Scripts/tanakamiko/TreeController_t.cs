using System.Collections;
using UnityEngine;

/// <summary>
/// Tree_Frag をクリックしたら木を生成する
/// Canvasなし（ワールド配置の札）対応版
/// ・最初の1回のみクールタイムなし
/// ・押し込み演出（Scale変更）
/// ・クールタイム中は暗く表示
/// ・マウスを札に置いたら「生成予測（透け木）」を表示
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

    public Transform demoParent;        // ★デモで生成した木を入れる親（TreeDemoRoot）

    // ====== 追加：生成予測（ゴースト） ======
    [Header("生成予測（ゴースト表示）")]
    public bool enablePreview = true;
    public float previewAlpha = 0.35f;
    public Color previewReadyColor = Color.white;
    public Color previewCooldownColor = new Color(1f, 0.4f, 0.4f, 1f); // クール中は赤っぽく等（好みで）
    public bool hidePreviewDuringCooldown = false; // trueにするとクール中は予測表示しない

    private GameObject previewObj;
    private SpriteRenderer previewSR;
    private bool isHoveringCard = false;

    private float passedTime = 0f;
    private bool firstUse = true;
    private bool isPressing = false;

    void Start()
    {
        passedTime = cooldown;

        if (cardTransform != null)
            cardTransform.localScale = normalScale;

        if (cardRenderer != null)
            cardRenderer.color = readyColor;

        // ゴースト初期化
        if (enablePreview)
        {
            CreatePreviewObject();
            SetPreviewVisible(false);
        }
    }

    void Update()
    {
        passedTime += Time.deltaTime;
        UpdateCardColor();

        // ★ホバー判定（札の上にマウスがあるか）
        UpdateHoverState();

        // ★予測表示の更新
        UpdatePreview();

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
    // ホバー判定（札の上にマウスがあるか）
    //==============================
    private void UpdateHoverState()
    {
        if (!enablePreview) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        isHoveringCard = (hit.collider != null && hit.collider.CompareTag(targetTag));
    }

    //==============================
    // 押し込み演出
    //==============================
    private IEnumerator PressEffect()
    {
        isPressing = true;

        if (cardTransform != null)
        {
            cardTransform.localScale = pressedScale;
            yield return new WaitForSeconds(pressDuration * 0.5f);
            cardTransform.localScale = normalScale;
        }
        else
        {
            yield return new WaitForSeconds(pressDuration);
        }

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

        cardRenderer.color = (passedTime >= cooldown) ? readyColor : cooldownColor;
    }

    //==============================
    // 木生成処理（本番）
    //==============================
    private void SpawnTreeNow()
    {
        if (playerGate == null) return;

        Vector3 spawnPoint;
        bool ok = TryGetSpawnPoint(out spawnPoint);

        if (ok)
        {
            Transform parent = (demoParent != null) ? demoParent : null;
            GameObject tree = Instantiate(treePrefab, spawnPoint, Quaternion.identity, parent);
            StartCoroutine(GrowTree(tree.transform));
        }
        else
        {
            Debug.LogWarning("木を生成する地面が見つかりません。");
        }
    }

    //==============================
    // 生成地点を「本番と同じロジック」で取得
    //==============================
    private bool TryGetSpawnPoint(out Vector3 spawnPoint)
    {
        spawnPoint = Vector3.zero;
        if (playerGate == null) return false;

        float spawnX;

        if (demoGoal != null)
        {
            // DemoGoalの○マス前に固定
            spawnX = demoGoal.position.x - goalBackDistance;
        }
        else
        {
            // プレイヤーの向きで○マス先
            float dir = playerGate.localScale.x > 0 ? 1 : -1;
            spawnX = playerGate.position.x + (spawnDistance * dir);
        }

        // Ray開始位置（上から下へ地面探す）
        Vector3 rayStart = new Vector3(spawnX, playerGate.position.y + 5f, 0f);

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 10f, groundLayer);
        if (hit.collider == null) return false;

        spawnPoint = hit.point;
        return true;
    }

    //==============================
    // ゴースト（生成予測）作成
    //==============================
    private void CreatePreviewObject()
    {
        if (treePrefab == null) return;

        // treePrefabを複製してゴーストにする
        previewObj = Instantiate(treePrefab);
        previewObj.name = "[TreePreview]";
        previewObj.SetActive(false);

        // 当たり判定無効化（押し出し防止）
        foreach (var col in previewObj.GetComponentsInChildren<Collider2D>(true))
        {
            col.enabled = false;
        }

        // 見た目（SpriteRenderer）取得
        previewSR = previewObj.GetComponentInChildren<SpriteRenderer>(true);
        if (previewSR != null)
        {
            // 半透明にする
            Color c = previewReadyColor;
            c.a = previewAlpha;
            previewSR.color = c;
        }

        // 成長コルーチン等が付いていて動くのが嫌なら、Preview側だけ止めたい場合は
        // 木PrefabのGrowスクリプトを「previewObjから外す」などの対応が必要です。
        // まずは「表示だけ」でOKになるよう、Scaleを固定します：
        previewObj.transform.localScale = new Vector3(treeWidth, growHeight, 1f);
    }

    private void SetPreviewVisible(bool visible)
    {
        if (previewObj == null) return;
        previewObj.SetActive(visible);
    }

    //==============================
    // ゴースト（生成予測）更新
    //==============================
    private void UpdatePreview()
    {
        if (!enablePreview) return;
        if (previewObj == null) return;

        // 札に乗ってないなら非表示
        if (!isHoveringCard)
        {
            SetPreviewVisible(false);
            return;
        }

        // クール中は表示しない設定なら非表示
        if (!firstUse && hidePreviewDuringCooldown && passedTime < cooldown)
        {
            SetPreviewVisible(false);
            return;
        }

        // 生成地点が取れないなら非表示
        Vector3 p;
        if (!TryGetSpawnPoint(out p))
        {
            SetPreviewVisible(false);
            return;
        }

        // 位置更新＆表示
        previewObj.transform.position = p;
        SetPreviewVisible(true);

        // 色（クール中は色を変える）
        if (previewSR != null)
        {
            bool ready = firstUse || passedTime >= cooldown;
            Color baseC = ready ? previewReadyColor : previewCooldownColor;
            baseC.a = previewAlpha;
            previewSR.color = baseC;
        }

        // 表示用Scale（常に最終サイズの「透け木」で見せる）
        previewObj.transform.localScale = new Vector3(treeWidth, growHeight, 1f);
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
            tree.localScale = Vector3.Lerp(startScale, endScale, time / growTime);
            time += Time.deltaTime;
            yield return null;
        }

        tree.localScale = endScale;
    }

    //==============================
    // 外部から木生成を呼ぶ
    //==============================
    public void DemoSpawnTree()
    {
        SpawnTreeNow();
    }

    private void OnDisable()
    {
        // シーン切替等で残るのが嫌なら消す
        if (previewObj != null)
        {
            Destroy(previewObj);
            previewObj = null;
        }
    }
}
