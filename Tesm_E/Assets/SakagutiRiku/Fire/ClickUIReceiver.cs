using UnityEngine;

public class ClickUIReceiver : MonoBehaviour
{
    [SerializeField] private GameObject targetUI; // �o����������UI

    private void OnEnable()
    {
        ClickableObject.OnObjectClicked += ShowUI;
    }

    private void OnDisable()
    {
        ClickableObject.OnObjectClicked -= ShowUI;
    }

    private void ShowUI()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(true);
            Debug.Log("UI���\������܂����I");
        }
    }
}
