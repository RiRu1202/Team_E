using UnityEngine;

public class PanelEnemyForceShow_t : MonoBehaviour
{
    [Header("‚±‚Ìƒpƒlƒ‹‚Å•\¦‚µ‚½‚¢“G")]
    public GameObject enemyRoot;          // —áFHitodama_Demo or Oni_Demo
    public SpriteRenderer enemyRenderer;  // “G‚ÌSpriteRenderer
    public Collider2D enemyCollider;      // “G‚ÌCollider2D

    void OnEnable()
    {
        // ƒpƒlƒ‹‚ğŠJ‚¢‚½‚ç•K‚¸“G‚ğgŒ©‚¦‚é•“–‚½‚éhó‘Ô‚É–ß‚·
        if (enemyRoot != null) enemyRoot.SetActive(true);
        if (enemyRenderer != null) enemyRenderer.enabled = true;
        if (enemyCollider != null) enemyCollider.enabled = true;
    }
}
