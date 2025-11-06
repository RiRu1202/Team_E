using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickTimer_s: MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;  // 対象レイヤー
    [SerializeField] private Canvas canvas;          // タイマーを表示するキャンバス
    [SerializeField] private GameObject timerPrefab; // タイマーUIのPrefab
    [SerializeField] private float timerDuration = 3f; // 秒数

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                // 特定レイヤーのオブジェクトをクリックしたとき
                ShowTimer(hit.transform);
            }
        }
    }

    void ShowTimer(Transform target)
    {
        GameObject timerObj = Instantiate(timerPrefab, canvas.transform);
        TimerUI_s timerUI = timerObj.GetComponent<TimerUI_s>();
        timerUI.StartTimer(timerDuration, target);
    }
}
