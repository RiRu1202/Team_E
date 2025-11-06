using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerUI_s : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private float remainingTime;
    private Transform target;
    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }

    public void StartTimer(float duration, Transform targetObj)
    {
        target = targetObj;
        remainingTime = duration;
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
            yield return null;
        }
        Destroy(gameObject); // タイマーだけ削除
    }

    void Update()
    {
        // 対象オブジェクトの上に追従
        if (target != null)
        {
            Vector3 screenPos = mainCam.WorldToScreenPoint(target.position + Vector3.up * 2f);
            transform.position = screenPos;
        }
    }
}
