using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SaveLoadManager_t : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSE;
    public string nextSceneName;

    public void OnButtonClick()
    {
        StartCoroutine(PlaySEThenChangeScene());
    }
    private System.Collections.IEnumerator PlaySEThenChangeScene()
    {
        if (audioSource != null && clickSE != null)
        {
            audioSource.PlayOneShot(clickSE);
        }
        // 効果音の長さだけ待つ
        yield return new WaitForSeconds(clickSE.length);
        // シーン移動
        SceneManager.LoadScene(nextSceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

//public void OnButtonClick()
//{
//    if (audioSource != null && clickSE != null)
//    {
//        audioSource.PlayOneShot(clickSE);
//    }
//    SceneManager.LoadScene(nextSceneName);
//    SceneManager.sceneLoaded += OnSceneLoaded;
//}
void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == PlayerPrefs.GetString("SavedScene"))
        {
            // 死んだ敵だけロード → その敵だけ削除
            DeadEnemyData_t.LoadDeadEnemiesAndDestro();
            // プレイヤーを復元
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                float px = PlayerPrefs.GetFloat("PlayerX");
                float py = PlayerPrefs.GetFloat("PlayerY");
                playerObj.transform.position = new Vector3(px, py, 0);
            }
            // カメラ位置も復元
            Camera cam = Camera.main;
            if (cam != null)
            {
                float cx = PlayerPrefs.GetFloat("CameraX");
                float cy = PlayerPrefs.GetFloat("CameraY");
                cam.transform.position = new Vector3(cx, cy, -10);
            }
            // 1回だけ実行するため解除
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        //    // 効果音だけ鳴らす（これはOK）
        //    if (audioSource != null && clickSE != null)
        //    {
        //        audioSource.PlayOneShot(clickSE);
        //    }
        //    // 先にゲームシーンをロード（タイトル→ゲーム）
        //    SceneManager.LoadScene(nextSceneName);
        //    SceneManager.sceneLoaded += OnSceneLoaded;
        //}
        //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        //{
        //    // プレイヤーをゲームシーン内で探す
        //    GameObject playerObj = GameObject.FindWithTag("Player");
        //    if (playerObj != null)
        //    {
        //        float px = PlayerPrefs.GetFloat("PlayerX");
        //        float py = PlayerPrefs.GetFloat("PlayerY");
        //        Vector3 pos = playerObj.transform.position;
        //        playerObj.transform.position = new Vector3(px, py, pos.z);
        //    }
        //    // カメラもゲームシーンで復元
        //    Camera cam = Camera.main;
        //    if (cam != null)
        //    {
        //        float cx = PlayerPrefs.GetFloat("CameraX");
        //        float cy = PlayerPrefs.GetFloat("CameraY");
        //        Vector3 cpos = cam.transform.position;
        //        cam.transform.position = new Vector3(cx, cy, cpos.z);
        //    }
        //    // 1回だけ実行なので解除
        //    SceneManager.sceneLoaded -= OnSceneLoaded;
        //    Debug.Log("ロード完了しました");
    }
}

        //// ========== ここが敵を“殺す（Destroy）”処理 ==========
        //GameObject[] sceneEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //int savedAlive = PlayerPrefs.GetInt("EnemyCount", 0);
        //// ★「SavedAliveCount に含まれない敵」はすべてDestroy★
        //int currentIndex = 0;
        //for (int i = 0; i < sceneEnemies.Length; i++)
        //{
        //    if (currentIndex < savedAlive)
        //    {
        //        // 生きてた敵はそのまま（動かないので位置変更も不要 or したければ後で指定）
        //        currentIndex++;
        //        continue;
        //    }
        //    // ここでDestroy（＝セーブ時に死んでいた敵を“殺す”）
        //    Destroy(sceneEnemies[i]);
        //    Debug.Log("敵（" + sceneEnemies[i].name + "）をロードでDestroyしました（＝死亡状態復元）");
        //}
        //Debug.Log("ロード完了！（Save時に死んでたEnemyは復元Destroyで殺しました）");



    