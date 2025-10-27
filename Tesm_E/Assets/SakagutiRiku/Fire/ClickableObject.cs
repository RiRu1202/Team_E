using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnObjectClicked; // UIへ通知するイベント

    void OnMouseDown()
    {
        // 左クリック限定（OnMouseDownでは区別できないのでInputでチェック）
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"{gameObject.name} がクリックされました！");
            OnObjectClicked?.Invoke(); // イベントを発火（通知送信）
        }
    }
}
