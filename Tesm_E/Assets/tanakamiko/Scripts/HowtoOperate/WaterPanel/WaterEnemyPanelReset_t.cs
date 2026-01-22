using UnityEngine;

public class WaterEnemyPanelReset_t : MonoBehaviour
{
    public HitodamaRespawn_t hitodama;

    void OnEnable()
    {
        if (hitodama != null)
            hitodama.ForceReset();
    }
}
