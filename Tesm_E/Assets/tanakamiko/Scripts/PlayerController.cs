using UnityEngine;
using UnityEngine.SceneManagement;//シーンの切り替えに必要
public class PlayerController:MonoBehaviour
{
    Rigidbody2D rbody;       //Rigidbody2D型の変数
    public float speed = 3.0f;
    public float gameOverY = -10f;//ゲームオーバーと判断するY座標

    public static string gameState = "playing";  //ゲームの状態

    void Start()
    {
        Application.targetFrameRate = 60;

        //Rigidbody2Dを取ってくる
        rbody = GetComponent<Rigidbody2D>();
        gameState = "playing";  //ゲーム中にする
    }
    void Update()
    {
        if(gameState!="playing")
        {
            return;
        }

        //プレイヤーのY座標が設定値より低くなったら
        if(transform.position.y<gameOverY)
        {
            GameOver();
        }
        
    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        //速度を更新する
        rbody.linearVelocity = new Vector2(5.0f, 0);
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー！");
        //「GameOver」シーンを読み込む
        SceneManager.LoadScene("GameOva");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("Clear");
    }

}
