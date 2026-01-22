using UnityEngine;

public class WaterController_t_T : MonoBehaviour
{
    public GameObject waterPrefab;
    public Transform waterSpawnPoint;
    public float cooldown = 0.5f;

    private float lastTime = -999f;

    public void Water()
    {
        if (Time.time < lastTime + cooldown) return;

        Instantiate(waterPrefab, waterSpawnPoint.position, Quaternion.identity);
        lastTime = Time.time;
    }
}
