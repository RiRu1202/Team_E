using UnityEngine;

public class PanelBulletCleanup_t : MonoBehaviour
{
    public string bulletTag = "FireDemo"; // êÖÇ»ÇÁ WaterDemo Ç…Ç∑ÇÈ

    void OnDisable()
    {
        var bullets = GameObject.FindGameObjectsWithTag(bulletTag);
        for (int i = 0; i < bullets.Length; i++)
            Destroy(bullets[i]);
    }
}
