using UnityEngine;

/// <summary>
/// デモ用：落とし穴を埋める土（足場）を生成する
/// ・DemoGoal基準で「何マス手前」に固定生成
/// ・生成した土はdemoParent配下に入れる（ループで全削除しやすい）
/// </summary>
public class SoilController_t : MonoBehaviour
{
    [Header("生成する土")]
    public GameObject soilPrefab;

    [Header("基準")]
    public Transform demoGoal;              // DemoGoal
    public float goalBackDistance = 6f;     // DemoGoalの6マス前（必要なら変更）

    [Header("高さ")]
    public float soilY = 0f;                // 土を置くY（左地面と同じ高さに）

    [Header("デモ親")]
    public Transform demoParent;            // SoilDemoRoot

    GameObject currentSoil;

    // ★デモから呼ぶ
    public void DemoSpawnSoil()
    {
        // 既にあれば消して作り直し（安定）
        if (currentSoil != null) Destroy(currentSoil);

        if (soilPrefab == null || demoGoal == null)
        {
            Debug.LogWarning("SoilController_t: soilPrefab or demoGoal is missing");
            return;
        }

        float x = demoGoal.position.x - goalBackDistance;
        Vector3 pos = new Vector3(x, soilY, 0f);

        Transform parent = (demoParent != null) ? demoParent : null;
        currentSoil = Instantiate(soilPrefab, pos, Quaternion.identity, parent);
    }
}

