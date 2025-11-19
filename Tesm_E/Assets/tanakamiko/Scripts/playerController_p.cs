using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーンの切り替えに必要
public class PlayerController_p : MonoBehaviour
{
    public float speed = 3.0f;
    public float gameOverY = -10f;//ゲームオーバーと判断するY座標
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public static string gameState = "playing";  //ゲームの状態

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        //Rigidbody2Dを取ってくる
        rb = GetComponent<Rigidbody2D>();
        gameState = "playing";  //ゲーム中にする
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up *jumpForce, ForceMode2D.Impulse);
        }

        if (gameState != "playing")
        {
            return;
        }

        //プレイヤーのY座標が設定値より低くなったら
        if (transform.position.y < gameOverY)
        {
            GameOver();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        //速度を更新する
        rb.linearVelocity = new Vector2(5.0f, rb.linearVelocityY);
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー！");
        //「GameOver」シーンを読み込む
        SceneManager.LoadScene("GameOva");
    }
}