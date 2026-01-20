using System.Collections;
using UnityEngine;

/// <summary>
/// 土札デモ：走り出してから1秒後に
/// 札を押し込み演出 → 土生成（1回だけ）
/// ループのたびにStartCycle()を呼んで再実行する
/// </summary>
public class AutoClickSoilDemo_t : MonoBehaviour
{
    public SoilController_t soilController;

    [Header("タイミング")]
    public float spawnAfterRunStart = 1.0f;   // ★走り出してから1秒後

    [Header("札サイズ")]
    public Vector3 normalScale = new Vector3(1.2f, 1.2f, 1f);
    public Vector3 pressedScale = new Vector3(1.1f, 1.1f, 1f);
    public float pressDuration = 0.1f;

    Coroutine routine;

    void OnEnable()
    {
        StartCycle();
    }

    void OnDisable()
    {
        StopCycle();
    }

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
        yield return new WaitForSeconds(spawnAfterRunStart);

        transform.localScale = pressedScale;
        yield return new WaitForSeconds(pressDuration);
        transform.localScale = normalScale;

        if (soilController != null)
            soilController.DemoSpawnSoil();
    }
}

