using UnityEngine;

public class DestroyOnPlayerAttack_s : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 相手のタグが PlayerAttack のときだけこのオブジェクトを消す
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Destroy(gameObject);
        }
        // それ以外は何もしない
    }
}
