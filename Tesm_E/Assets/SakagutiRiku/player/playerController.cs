using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController:MonoBehaviour
{
    Rigidbody2D rbody;            //Rigidbody2D型の変数
    public float speed = 3.0f;　　//移動速度

    public float jump = 6.0f;     //ジャンプ力
    public LayerMask groundLayer; //着地できるレイヤー
    bool goJump = false;          //ジャンプ開始フラグ

    private void Update()
    {
        //キャラクターをジャンプさせる
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
            Debug.Log("ジャンプ");
        }
    }

    private void FixedUpdate()
    {
        //�n�㔻��
        bool onGround = Physics2D.CircleCast(transform.position,   //発射位置
                                           0.2f,                 //円の半径
                                           Vector2.down,         //発射方向
                                           0.0f,                 //発射距離
                                           groundLayer);         //検出するレイヤー

        if(onGround)
        {
            //地面の上
            //速度を更新する
            rbody.linearVelocity = new Vector2(speed , rbody.linearVelocity.y);
        }
        if(onGround&&goJump)
        {
            //地面の上でジャンプキーが押された
            //ジャンプさせる
            Vector2 jumpPw = new Vector2(0, jump);       //ジャンプさせるベクトルを作る
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); //瞬間的な力を加える
            goJump = false; //ジャンプフラグを下ろす
        }
    }
    //ジャンプ
    public void Jump()
    {
        goJump = true; //ジャンプフラグを立てる
    }
}
