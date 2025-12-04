using UnityEngine;

public class DestroyWhenInvisible_t : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ‰½‚©‚ÉÕ“Ë‚µ‚½‚çÁ‚·
        Destroy(gameObject);
    }

    // ‚à‚µTrigger‚Ìê‡‚Í‰º‚ğg‚¤
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
    */
}
