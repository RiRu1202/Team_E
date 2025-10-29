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

        Debug.Log("UI�؂�ւ��J�n");

        if (uiPrefab == null || playerUI == null)
        {
            Debug.LogError("uiPrefab �܂��� playerUI �����ݒ�ł��I");
            yield break;
        }

        Canvas canvas = FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas ��������܂���I");
            yield break;
        }

        playerUI.SetActive(false);  // ����ŃX�N���v�g���~�܂�\������
        currentUI = Instantiate(uiPrefab, canvas.transform);
        currentUI.transform.position = playerUI.transform.position;

        Debug.Log("�ҋ@�J�n");
        yield return new WaitForSecondsRealtime(duration);  // �� Realtime �ɕύX
        Debug.Log("�ҋ@�I��");

        if (currentUI != null)
            Destroy(currentUI);

        playerUI.SetActive(true);
        Debug.Log("UI�߂�����");

        isSwitching = false;
    }
}
