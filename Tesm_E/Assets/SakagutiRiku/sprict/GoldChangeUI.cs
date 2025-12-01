using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldChangeUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public GameObject playerUI;
    public string targetUIButtonName = "GoldButton";
    public float duration = 2f;

    private GameObject currentUI;
    private bool isSwitching = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedUI(targetUIButtonName))
            {
                StartCoroutine(ShowTemporaryUI());
            }
        }
    }

    private bool IsClickedUI(string targetUIName)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.name == targetUIName)
                return true;
        }

        return false;
    }

    private IEnumerator ShowTemporaryUI()
    {
        if (isSwitching) yield break;
        isSwitching = true;

        Debug.Log("UI切り替え開始");

        if (uiPrefab == null || playerUI == null)
        {
            Debug.LogError("uiPrefab または playerUI が未設定です！");
            yield break;
        }

        Canvas canvas = FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas が見つかりません！");
            yield break;
        }

        playerUI.SetActive(false);  // これでスクリプトが止まる可能性あり
        currentUI = Instantiate(uiPrefab, canvas.transform);
        currentUI.transform.position = playerUI.transform.position;

        Debug.Log("待機開始");
        yield return new WaitForSecondsRealtime(duration);  // ← Realtime に変更
        Debug.Log("待機終了");

        if (currentUI != null)
            Destroy(currentUI);

        playerUI.SetActive(true);
        Debug.Log("UI戻し完了");

        isSwitching = false;
    }
}
