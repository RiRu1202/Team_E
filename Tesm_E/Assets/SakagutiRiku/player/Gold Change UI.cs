using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldChangeUI : MonoBehaviour
{
    public GameObject uiPrefab;     // 切り替え用UI（Prefab）
    public GameObject playerUI;     // 元UI
    public string targetUIButtonName = "GoldButton"; // クリック判定するUIの名前
    public float duration = 2f;     // 切り替えUIの表示時間

    private GameObject currentUI;

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

    // クリックされたUIが targetUIButtonName か判定
    private bool IsClickedUI(string targetUIName)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == targetUIName)
                return true;
        }

        return false;
    }

    private IEnumerator ShowTemporaryUI()
    {
        // Nullチェック
        if (uiPrefab == null)
        {
            Debug.LogError("uiPrefabがアサインされていません！");
            yield break;
        }

        if (playerUI == null)
        {
            Debug.LogError("playerUIがアサインされていません！");
            yield break;
        }

        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("CanvasがHierarchyに存在しません！");
            yield break;
        }

        // 元UIを非表示
        playerUI.SetActive(false);

        // 切り替えUIを生成
        currentUI = Instantiate(uiPrefab, canvas.transform);
        currentUI.transform.position = playerUI.transform.position;

        // duration 秒待つ
        yield return new WaitForSeconds(duration);

        // 元UIに戻す
        if (currentUI != null)
            Destroy(currentUI);

        playerUI.SetActive(true);
    }
}
