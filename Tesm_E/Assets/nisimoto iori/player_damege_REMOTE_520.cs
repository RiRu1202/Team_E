using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_damege:MonoBehaviour
{

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
    
}
