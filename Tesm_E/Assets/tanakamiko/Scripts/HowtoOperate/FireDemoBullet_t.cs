using UnityEngine;

public class FireDemoBullet_t : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("oniDemo"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

