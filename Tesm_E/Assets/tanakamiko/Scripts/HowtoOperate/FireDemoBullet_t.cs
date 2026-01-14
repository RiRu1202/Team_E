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
            // ‹S‚Éu€‚ñ‚Å‚Ëv‚Æ“`‚¦‚é
            OniRespawn_t respawn = other.GetComponent<OniRespawn_t>();
            if (respawn != null)
            {
                respawn.Die();
            }

            Destroy(gameObject);
        }
    }
}


//using UnityEngine;
//using System.Collections;

//public class FireDemoBullet_t : MonoBehaviour
//{
//    public float speed = 3f;
//    public float respawnDelay = 2.0f;   // šÁ–Å‚µ‚Ä‚©‚ç•œŠˆ‚Ü‚Å‚ÌŠÔ

//    void Update()
//    {
//        transform.Translate(Vector2.right * speed * Time.deltaTime);
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("oniDemo"))
//        {
//            // ‹S‚ğÁ‚·
//            other.gameObject.SetActive(false);

//            // 2•bŒã‚É•œŠˆ
//            StartCoroutine(RespawnOni(other.gameObject));

//            // ‰Š‚ÍÁ–Å
//            Destroy(gameObject);
//        }
//    }

//    IEnumerator RespawnOni(GameObject oni)
//    {
//        yield return new WaitForSeconds(respawnDelay);
//        oni.SetActive(true);
//    }
//}

