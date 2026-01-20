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
            // ãSÇ…ÅuéÄÇÒÇ≈ÇÀÅvÇ∆ì`Ç¶ÇÈ
            OniRespawn_t respawn = other.GetComponent<OniRespawn_t>();
            if (respawn != null)
            {
                respawn.Die();
            }

            Destroy(gameObject);
        }
    }
}




