using UnityEngine;

public class FryEnemyDeath_n : MonoBehaviour
{
    //攻撃が当たった時に呼ばれるメソッド
    private void OnCollisionEnter2D(Collision2D other)
    {
        //衝突した相手のオブジェクトが「PlayerAttack」タグを持っていたら
        if (other.gameObject.tag == "PlayerwaterAttack")
        {

            //このスクリプトがアタッチされているゲームオブジェクト（敵）
            Destroy(gameObject);
        }

    }
}
