using UnityEngine;

public class FireEnemyPanelReset_t : MonoBehaviour
{
    public OniRespawn_t oni;

    void OnEnable()
    {
        if (oni != null)
            oni.ForceReset();   // š•K‚¸‰Šúó‘Ô‚É–ß‚·
    }
}
