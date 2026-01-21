using System.Collections;
using UnityEngine;

public class AutoClickTreeDemo_t : MonoBehaviour
{
    public TreeController_t treeController;

    [Header("タイミング")]
    public float spawnAfterRunStart = 1.0f;   // ★走り出してから1秒後

    [Header("札サイズ")]
    public Vector3 normalScale = new Vector3(1.2f, 1.2f, 1f);
    public Vector3 pressedScale = new Vector3(1.1f, 1.1f, 1f);
    public float pressDuration = 0.1f;

    Coroutine routine;

    void OnEnable()
    {
        // パネル表示開始＝周回開始扱い
        StartCycle();
    }

    void OnDisable()
    {
        StopCycle();
    }

    // ★周回開始（外から呼べる）
    public void StartCycle()
    {
        StopCycle();
        routine = StartCoroutine(Cycle());
    }

    void StopCycle()
    {
        if (routine != null) StopCoroutine(routine);
        routine = null;
        transform.localScale = normalScale;
    }

    IEnumerator Cycle()
    {
        // 走り出してから1秒後
        yield return new WaitForSeconds(spawnAfterRunStart);

        // 押し込み演出
        transform.localScale = pressedScale;
        yield return new WaitForSeconds(pressDuration);
        transform.localScale = normalScale;

        // 木生成（1回だけ）
        if (treeController != null)
        {
            treeController.DemoSpawnTree();
        }
    }
}
