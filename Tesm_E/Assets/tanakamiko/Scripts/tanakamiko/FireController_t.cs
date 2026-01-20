using System.Collections;
using UnityEngine;

public class FireController_t : MonoBehaviour
{
    [Header("発射設定")]
    public GameObject firePrefab;
    public float cooldown = 1f;
    public float fireSpeed = 4f;
    public Transform playerGate;

    [Header("札設定（Canvasなし）")]
    public string targetTag = "Fire_Frag";
    public Transform cardTransform;
    public SpriteRenderer cardRenderer;

    [Header("札サイズ")]
    public Vector3 normalScale = new Vector3(0.8f, 0.8f, 1f);
    public Vector3 pressedScale = new Vector3(0.7f, 0.7f, 1f);
    public float pressDuration = 0.1f;

    [Header("色")]
    public Color readyColor = Color.white;
    public Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    float timer;
    bool isPressing;

    void Start()
    {
        timer = cooldown;
        cardTransform.localScale = normalScale;
        cardRenderer.color = readyColor;
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateColor();

        if (!Input.GetMouseButtonDown(0)) return;
        if (!IsClicked()) return;
        if (timer < cooldown || isPressing) return;

        StartCoroutine(Press());
        Shoot();
        timer = 0f;
    }

    bool IsClicked()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        return hit.collider != null && hit.collider.CompareTag(targetTag);
    }

    IEnumerator Press()
    {
        isPressing = true;
        cardTransform.localScale = pressedScale;
        yield return new WaitForSeconds(pressDuration * 0.5f);
        cardTransform.localScale = normalScale;
        isPressing = false;
    }

    void UpdateColor()
    {
        cardRenderer.color = timer >= cooldown ? readyColor : cooldownColor;
    }

    void Shoot()
    {
        GameObject obj = Instantiate(firePrefab, playerGate.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(playerGate.right * fireSpeed, ForceMode2D.Impulse);
    }
}
