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
    void OnCollisionEnter2D(Collision2D collision)//�Փ˔���
    {
        if (collision.gameObject.tag == "Enemy")//�^�O�̐ݒ�
        {
            Debug.Log("�����������[�I");
            float dirx = transform.position.x - collision.transform.position.x;
            Vector2 knockbackDir = new Vector2(Mathf.Sign(dirx), 0);
            StartCoroutine(ApplyKnockback(knockbackDir));
        }

        if (collision.gameObject.tag == "wall")//�^�O�̐ݒ�
        {
            Debug.Log("�����������[�I");
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
            // Y�����̑��x�͈ێ����āAX���������m�b�N�o�b�N
            rb.linearVelocity = new Vector2(direction.x * knockbackForce, rb.linearVelocity.y);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isKnockback = false; // �m�b�N�o�b�N�I��
    }
}
