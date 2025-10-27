using UnityEngine;

public class DestroyWhenInvisible : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ɏՓ˂��������
        Destroy(gameObject);
    }

    // ����Trigger�̏ꍇ�͉����g��
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
    */
}
