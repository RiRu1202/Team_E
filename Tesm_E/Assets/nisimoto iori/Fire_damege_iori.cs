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

    void OnCollisionEnter2D(Collision2D collision)//�Փ˔���
    {
        if (collision.gameObject.tag == "Trap")//���ƏՓ˂������m�F
        {
            Debug.Log("�����������[�I");
            Destroy(gameObject);//�΂̃I�u�W�F�N�g���f���[�g
        }
        else if (collision.gameObject.tag == "ground")//�n�ʂɓ�����Ə���
        {
            Destroy(gameObject);
        }

    }
}