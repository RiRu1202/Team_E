using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fair_damege_iori : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)//衝突判定
    {
        if (collision.gameObject.tag == "Trap")//罠と衝突したか確認
        {
            Debug.Log("反応したぞー！");
            Destroy(gameObject);//火のオブジェクトをデリート
        }
        else if (collision.gameObject.tag == "ground")//地面に当たると消滅
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "wall")//壁に当たると消滅
        {
            Destroy(gameObject);
        }
    }
}