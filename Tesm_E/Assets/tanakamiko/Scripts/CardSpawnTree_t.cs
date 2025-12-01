using System.Collections;
using UnityEngine;

public class CardSpawnTree_t : MonoBehaviour
{
    public GameObject treePrefab;      // 木のプレハブ
    public Transform Player;           // 主人公
    public float spawnDistance = 3f;   // 3マス先
    public float cooldown = 0.5f;      // クールタイム
    public LayerMask groundLayer;   // 地面レイヤー
    public AudioClip spawnSound;
    public AudioSource audioSource;

    private bool canSpawn = true;
    void OnMouseDown()   // 札をクリックした時
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnTree());
        }
    }
    IEnumerator SpawnTree()
    {
        canSpawn = false;

        // 向いている方向
        float dir = Player.localScale.x > 0 ? 1 : -1;
        // Xは3マス先、Yは高めからRayを飛ばす
        Vector3 rayStart = Player.position + new Vector3(spawnDistance * dir, 5f, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 10f, groundLayer);
        if (hit.collider != null)
        {
            Instantiate(treePrefab, hit.point, Quaternion.identity);

            //効果音再生
            if(audioSource != null && spawnSound != null)
            {
                audioSource.PlayOneShot(spawnSound);
            }
        }
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }
}