using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUIManager_s : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject timerTextPrefab; // TextMeshProUGUI プレハブ

    private Dictionary<TargetWithTimer_s, TextMeshProUGUI> activeTimers = new();

    void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    public void ShowTimerDisplay(TargetWithTimer_s target, Vector3 worldPos, float time)
    {
        if (!activeTimers.ContainsKey(target))
        {
            GameObject textObj = Instantiate(timerTextPrefab, canvas.transform);
            var text = textObj.GetComponent<TextMeshProUGUI>();
            text.text = $"{time:F1}";
            activeTimers.Add(target, text);
        }
    }

    public void UpdateTimerDisplay(TargetWithTimer_s target, float time)
    {
        if (activeTimers.TryGetValue(target, out var text))
        {
            // オブジェクトの位置をスクリーン座標に変換
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.transform.position + Vector3.up * 2f);
            text.transform.position = screenPos;
            text.text = $"{Mathf.Max(time, 0f):F1}";
        }
    }

    public void HideTimerDisplay(TargetWithTimer_s target)
    {
        if (activeTimers.TryGetValue(target, out var text))
        {
            Destroy(text.gameObject);
            activeTimers.Remove(target);
        }
    }
}
