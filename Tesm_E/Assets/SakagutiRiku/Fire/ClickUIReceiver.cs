using UnityEngine;

public class ClickUIReceiver : MonoBehaviour
{
    [SerializeField] private GameObject targetUI; // èoåªÇ≥ÇπÇΩÇ¢UI

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
            Debug.Log("UIÇ™ï\é¶Ç≥ÇÍÇ‹ÇµÇΩÅI");
        }
    }
}
