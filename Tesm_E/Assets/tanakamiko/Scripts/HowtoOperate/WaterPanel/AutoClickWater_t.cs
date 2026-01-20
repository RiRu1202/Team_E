using System.Collections;
using UnityEngine;

public class AutoClickWater_t_T : MonoBehaviour
{
    public WaterController_t_T waterController;

    [Header("発射間隔")]
    public float firstDelay = 2.0f; // 最初だけ2秒
    public float interval = 2.5f;

    [Header("札サイズ")]
    public Vector3 normalScale = new Vector3(1.2f, 1.2f, 1f);
    public Vector3 pressedScale = new Vector3(1.1f, 1.1f, 1f);
    public float pressDuration = 0.1f;

    Coroutine loop;

    void Awake()
    {
        transform.localScale = normalScale;
    }

    void OnEnable()
    {
        loop = StartCoroutine(AutoLoop());
    }

    void OnDisable()
    {
        if (loop != null) StopCoroutine(loop);
        loop = null;
        transform.localScale = normalScale;

        // ★残った水弾を消したいなら：WaterPrefabにタグ WaterDemo を付けて使う
        foreach (var b in GameObject.FindGameObjectsWithTag("WaterDemo"))
            Destroy(b);
    }

    IEnumerator AutoLoop()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            // 押し込み
            transform.localScale = pressedScale;
            yield return new WaitForSeconds(pressDuration);
            transform.localScale = normalScale;

            // 発射
            if (waterController != null) waterController.Fire();

            yield return new WaitForSeconds(interval);
        }
    }
}
