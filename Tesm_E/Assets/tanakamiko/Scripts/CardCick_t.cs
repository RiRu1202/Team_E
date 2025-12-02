using UnityEngine;
public class CardClick_t : MonoBehaviour
{
    public GameObject flamePrefab; // Inspectorで自作炎のPrefabをセット
    public Transform playerHand;   // プレイヤーの手のTransform
    private void OnMouseDown()
    {
        ShootFlame();
    }
    void ShootFlame()
    {
        // 手の位置に炎を生成
        GameObject flame = Instantiate(flamePrefab, playerHand.position, Quaternion.identity);
        // Rigidbody2Dがあれば前方に飛ばす
        Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.right * 10f; // 速度は調整
        }
        // 自動で一定時間後に消す
        Destroy(flame, 2f);
    }
}