using UnityEngine;

public class EnemyDeath_s:MonoBehaviour
{
    //�U���������������ɌĂ΂�郁�\�b�h
    private void OnCollisionEnter2D(Collision2D other)
    {
        //�Փ˂�������̃I�u�W�F�N�g���uPlayerAttack�v�^�O�������Ă�����
        if (other.gameObject.tag == "PlayerAttack")
        {
          
            //���̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�i�G�j
            Destroy(gameObject);
        }

    }
}
