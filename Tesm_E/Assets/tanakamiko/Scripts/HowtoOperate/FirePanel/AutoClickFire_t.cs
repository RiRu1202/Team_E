using System.Collections;
using UnityEngine;

public class AutoClickFire_t : MonoBehaviour
{
    public FireController_Demo_t fireController;

    [Header("対象")]
    public GameObject oniDemo;   // 復活させる鬼

    [Header("発射間隔")]
    public float firstDelay = 2.0f;
    public float interval = 10f;

    [Header("札サイズ")]
    public Vector3 normalScale = new Vector3(0.5f, 0.5f, 1f);
    public Vector3 pressedScale = new Vector3(0.45f, 0.45f, 1f);
    public float pressDuration = 0.1f;

    Coroutine loop;
    Transform cardTransform;

    void Awake()
    {
        cardTransform = transform;
        cardTransform.localScale = normalScale;
    }

    void OnEnable()
    {
        loop = StartCoroutine(AutoFireLoop());
    }

    void OnDisable()
    {
        // 自動発射ループ停止
        if (loop != null)
        {
            StopCoroutine(loop);
            loop = null;
        }

        // ★残っている炎を全削除
        foreach (var fire in GameObject.FindGameObjectsWithTag("FireDemo"))
        {
            Destroy(fire);
        }
    }


    IEnumerator AutoFireLoop()
    {
        // 最初の1回だけ待つ
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            // 札の押し込み演出
            yield return StartCoroutine(PressAnimation());

            // 炎を発射
            fireController.Fire();

            // 次の発射まで待つ
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator PressAnimation()
    {
        cardTransform.localScale = pressedScale;
        yield return new WaitForSeconds(pressDuration);
        cardTransform.localScale = normalScale;
    }
}

    