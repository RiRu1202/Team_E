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

    
    void OnCollisionEnter2D(Collision2D collision)//�Փ˔���
    {
        if (collision.gameObject.tag == "trap")//�^�O�̐ݒ�
        {
            Debug.Log("�����������[�I");
        }
    }
}
