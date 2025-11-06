using UnityEngine;

public class TargetWithTimer_s : MonoBehaviour
{
    [SerializeField] private string targetLayerName = "TargetLayer"; // クリック対象レイヤー
    [SerializeField] private float countdownTime = 3f; // タイマー秒数

    private bool isCountingDown = false;
    private float timer;
    private CountdownUIManager_s uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<CountdownUIManager_s>();
    }

    void Update()
    {
        // 左クリック検出
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }

        // カウントダウン進行
        if (isCountingDown)
        {
            timer -= Time.deltaTime;
            uiManager.UpdateTimerDisplay(this, timer);

            if (timer <= 0f)
            {
                // タイマーが0になったらUIだけ消す
                isCountingDown = false;
                uiManager.HideTimerDisplay(this);
            }
        }
    }

    private void CheckClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject &&
                hit.collider.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
            {
                StartCountdown();
            }
        }
    }

    private void StartCountdown()
    {

        if (!isCountingDown)
        {
            isCountingDown = true;
            timer = countdownTime;
            Debug.Log($"[DEBUG] {gameObject.name} のタイマー開始！");
            uiManager.ShowTimerDisplay(this, transform.position, timer);
        }
    }
}
