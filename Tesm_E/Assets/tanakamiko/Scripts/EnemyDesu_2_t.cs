using UnityEngine;

public class EnemyDesu_2_t : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerwaterAttack"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Debug.Log("ìGÇ™êÖÇ≈è¡ñ≈ÇµÇ‹ÇµÇΩÅI");
        }
    }
}

