using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーンの切り替えに必要
public class PlayerController_t:MonoBehaviour
{
    Rigidbody2D rbody;       //Rigidbody2D型の変数
    public float speed = 3.0f;

    //ジャンプ設定
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    public float gameOverY = -10f;//ゲームオーバーと判断するY座標

    public static string gameState = "playing";  //ゲームの状態

    void Start()
    {
        //Rigidbody2Dを取ってくる
        rbody = GetComponent<Rigidbody2D>();
        gameState = "playing";  //ゲーム中にする
    }

    [System.Obsolete]
    void Update()
    {
        if (gameState != "playing")
        {
            return;
        }

        //地面判定チェック
        isGrounded = Physics2D.OverlapCircle
            (groundCheck.position,
            groundCheckRadius,
            groundLayer
            );

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbody.linearVelocity = new Vector2(rbody.linearVelocity.x, jumpForce);
        }

        //プレイヤーのY座標が設定値より低くなったら
        if (transform.position.y < gameOverY)
        {
            GameOver();
        }

    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        //速度を更新する
        rbody.linearVelocity = new Vector2(5.0f, rbody.linearVelocity.y);
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー！");
        //「GameOver」シーンを読み込む
        SceneManager.LoadScene("GameOva");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        }
    }
}
