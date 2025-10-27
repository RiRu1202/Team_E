using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldChangeUI : MonoBehaviour
{
    public GameObject uiPrefab;     // �؂�ւ��pUI�iPrefab�j
    public GameObject playerUI;     // ��UI
    public string targetUIButtonName = "GoldButton"; // �N���b�N���肷��UI�̖��O
    public float duration = 2f;     // �؂�ւ�UI�̕\������

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

    // �N���b�N���ꂽUI�� targetUIButtonName ������
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
        // Null�`�F�b�N
        if (uiPrefab == null)
        {
            Debug.LogError("uiPrefab���A�T�C������Ă��܂���I");
            yield break;
        }

        if (playerUI == null)
        {
            Debug.LogError("playerUI���A�T�C������Ă��܂���I");
            yield break;
        }

        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("Canvas��Hierarchy�ɑ��݂��܂���I");
            yield break;
        }

        // ��UI���\��
        playerUI.SetActive(false);

        // �؂�ւ�UI�𐶐�
        currentUI = Instantiate(uiPrefab, canvas.transform);
        currentUI.transform.position = playerUI.transform.position;

        // duration �b�҂�
        yield return new WaitForSeconds(duration);

        // ��UI�ɖ߂�
        if (currentUI != null)
            Destroy(currentUI);

        playerUI.SetActive(true);
    }
}
