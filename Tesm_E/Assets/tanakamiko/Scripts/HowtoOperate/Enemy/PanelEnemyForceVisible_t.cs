using UnityEngine;

public class PanelEnemyForceVisible_t : MonoBehaviour
{
    public DemoEnemyResetVisible_t enemy;

    void OnEnable()
    {
        if (enemy != null) enemy.ForceVisible();
    }
}
