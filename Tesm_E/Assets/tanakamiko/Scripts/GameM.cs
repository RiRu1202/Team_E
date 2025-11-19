using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  //UIを使うのに必要
public class GameM : MonoBehaviour
{
    public string gameOverSceneName = "GameOva";

    //+++時間制限追加+++
    public GameObject timeText;       //時間テキスト
    TimeController timeCnt;           //TimeController

    void Start()
    {
        //+++時間制限追加+++
        //TimeControllerを取得
        timeCnt = GetComponent<TimeController>();
    }
    void Update()
    {
        
        if (PlayerController_p.gameState=="playing")
        {
            //ゲーム中
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerControllerを取得する
            PlayerController_p playerCnt = player.GetComponent<PlayerController_p>();
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
