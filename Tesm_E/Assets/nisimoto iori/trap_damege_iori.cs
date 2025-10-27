using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_damege_iori : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)//衝突判定
    {
        if (collision.gameObject.tag == "Fire")//何と衝突したか確認
        {
            Debug.Log("反応したぞー！");
            Destroy(gameObject);//障害物のオブジェクトをデリート
        }


    }
}

