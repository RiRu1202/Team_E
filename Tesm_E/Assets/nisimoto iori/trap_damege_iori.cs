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

    void OnCollisionEnter2D(Collision2D collision)//�Փ˔���
    {
        if (collision.gameObject.tag == "Fire")//���ƏՓ˂������m�F
        {
            Debug.Log("�����������[�I");
            Destroy(gameObject);//��Q���̃I�u�W�F�N�g���f���[�g
        }


    }
}

