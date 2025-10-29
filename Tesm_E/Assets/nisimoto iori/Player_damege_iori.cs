using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_damege_iori:MonoBehaviour
{
    public float knockbackForce = 3.0f;
    public float knockbackDuration = 0.3f;
    private Rigidbody2D rb;
    private bool isKnockback = false;
    void Start()
    {

    }

    void Update()
    {

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)//衝突判定
    {
        if (collision.gameObject.tag == "trap")//タグの設定
        {
            Debug.Log("反応したぞー！");
            float dirx = transform.position.x - collision.transform.position.x;
            Vector2 knockbackDir = new Vector2(Mathf.Sign(dirx), 0);
            StartCoroutine(ApplyKnockback(knockbackDir));
        }
        else if (collision.gameObject.tag == "wall")//タグの設定
        {
            Debug.Log("反応したぞー！");
            float dirx = transform.position.x - collision.transform.position.x;
            Vector2 knockbackDir = new Vector2(Mathf.Sign(dirx), 0);
            StartCoroutine(ApplyKnockback(knockbackDir));
        }
    }

    IEnumerator ApplyKnockback(Vector2 direction)
    {
        isKnockback = true;

        float timer = 0f;
        while (timer < knockbackDuration)
        {
            // Y方向の速度は維持して、X方向だけノックバック
            rb.velocity = new Vector2(direction.x * knockbackForce, rb.velocity.y);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isKnockback = false; // ノックバック終了
    }
}
