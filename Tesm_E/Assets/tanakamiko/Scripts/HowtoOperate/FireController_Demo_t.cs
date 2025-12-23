using UnityEngine;

public class FireController_Demo_t : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform fireSpawnPoint;
    public float cooldown = 0.5f;

    private float lastFireTime = -999f;

    public void Fire()
    {
        if (Time.time < lastFireTime + cooldown) return;

        Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity);
        lastFireTime = Time.time;
    }
    //public GameObject firePrefab;
    //public Transform fireSpawnPoint;

    //public void Fire()
    //{
    //    Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity);
    //}
}
