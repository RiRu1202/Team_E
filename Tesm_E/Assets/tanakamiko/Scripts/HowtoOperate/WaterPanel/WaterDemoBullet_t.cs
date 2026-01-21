using UnityEngine;

public class WaterDemoBullet_t : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("水が何かに当たった: " + other.name + " Tag=" + other.tag);

        if (other.CompareTag("HitodamaDemo"))
        {
            Debug.Log("人魂に命中！");

            HitodamaRespawn_t respawn = other.GetComponent<HitodamaRespawn_t>();
            if (respawn != null)
            {
                respawn.Die();
                Debug.Log("Die() 呼び出し成功");
            }
            else
            {
                Debug.LogWarning("HitodamaRespawn_t が人魂についてない！");
            }

            Destroy(gameObject);
        }
    }
}
