using UnityEngine;

public class DestroyWhenInvisible_t : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // âΩÇ©Ç…è’ìÀÇµÇΩÇÁè¡Ç∑
        Destroy(gameObject);
    }
}
