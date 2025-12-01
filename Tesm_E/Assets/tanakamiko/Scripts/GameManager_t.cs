using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  //UIを使うのに必要
public class GameManager_t:MonoBehaviour
{
    public GameObject mainImage;      //画像を持つGameObject
    public string gameOverSceneName = "GameOva";

    Image titleImage;                 //画像を表示しているImageコンポーネント

    //+++時間制限追加+++
    public GameObject timeBar;        //時間表示イメージ
    public GameObject timeText;       //時間テキスト
    TimeController_t timeCnt;           //TimeController

    void Start()
    {
        //画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        
        //+++時間制限追加+++
        //TimeControllerを取得
        timeCnt = GetComponent<TimeController_t>();
        if (timeCnt != null)
        { 
           if (timeCnt.gameTime==0.0f)
           {
               timeBar.SetActive(false);  //制限時間なしなら隠す
           }
        }
    }
    void Update()
    {

         if (PlayerController_t.gameState=="playing")
        {
            //ゲーム中
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerControllerを取得する
            PlayerController_t playerCnt = player.GetComponent<PlayerController_t>();
            //+++時間制限追加+++
            //タイムを更新する
            if (timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    //整数に代入することで少数を切り捨てる
                    int time=(int)timeCnt.displayTime;
                    //タイム更新
                    if(timeText!=null)
                        timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();
                    //タイムオーバー
                    if(time <= 0)
                    {
                       
                       GameOver();  //ゲームオーバーにする
                    }
                }
            }
         }
    }
    void GameOver()
    {
        Debug.Log("GameOva");
        //シーンを切り替える
        SceneManager.LoadScene(gameOverSceneName);
    }
}
