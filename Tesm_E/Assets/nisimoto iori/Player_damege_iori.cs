using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_damege_iori:MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    
    void OnCollisionEnter2D(Collision2D collision)//衝突判定
    {
        if (collision.gameObject.tag == "trap")//タグの設定
        {
            Debug.Log("反応したぞー！");
        }
    }
}
