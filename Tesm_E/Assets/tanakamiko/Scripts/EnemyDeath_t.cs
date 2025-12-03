using UnityEngine;

public class EnemyDeath_t:MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Debug.Log("ìGÇ™âäÇ≈è¡ñ≈ÇµÇ‹ÇµÇΩÅI");
        }
    }
}
