using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RockController_P : MonoBehaviour
{
    public GameObject objPrefab;               // 生成するオブジェクト
    public float delayTime = 1f;               // 連射防止用
    public float rockDistance = 3f;            // プレイヤーからの距離（3マス先）
    public string targetUIButtonName = "Rock_Frag"; // クリック判定したいUIの名前

    private Transform gateTransform;           // プレイヤーの発射地点
    private float passedTime = 0f;

    void Start()
    {
        gateTransform = transform.Find("playergate");
    }

    void Update()
    {
        if (objPrefab == null || gateTransform == null) return;

        passedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedUI(targetUIButtonName))
            {
                if (passedTime >= delayTime)
                {
                    SpawnObject();
                    passedTime = 0f;
                }
            }
        }
    }

    // クリックしたUIが targetUIButtonName かチェック
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

    // プレイヤーの3マス先に objPrefab を生成
    private void SpawnObject()
    {
        Vector2 spawnPos = (Vector2)gateTransform.position + (Vector2)gateTransform.right * rockDistance;
        GameObject obj = Instantiate(objPrefab, spawnPos, Quaternion.identity);

        /*// Rigidbody2Dがついていれば物理で飛ばす（任意）
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            Vector2 dir = gateTransform.right;
            rbody.AddForce(dir * 4f, ForceMode2D.Impulse);
        }*/
    }
}
