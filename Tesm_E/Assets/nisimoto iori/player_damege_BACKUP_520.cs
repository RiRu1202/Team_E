using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_damege:MonoBehaviour
{
<<<<<<< HEAD
    public GameObject objPrefab;    //����������v���n�u�f�[�^
    public float delayTime = 3.0f;  //�x�����x
    public float firespeed = 4.0f;  //���ˑ��x
    public float length = 8.0f;     //�͈�

    GameObject player;
=======

    public float knockbackForce = 7.0f;
    private void Start()
    {
        
    }

    private void Update()
    {

        int a = 0;
        a++;
    }
    //public void OnDamage(Vector2 attackerPosition)
    //{
        
       
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    Vector2 knockbackDirection = (transform.position - (Vector3)attackerPosition).normalized;

      
    //    rb.linearVelocity = Vector2.zero; 
    //    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    //}

    //接触開始
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("反応");
        }

    }
    //public void trap()
    //{
    //    
    //}
    
>>>>>>> a225778343c823e0302081d76ef259d432da87ae
}
