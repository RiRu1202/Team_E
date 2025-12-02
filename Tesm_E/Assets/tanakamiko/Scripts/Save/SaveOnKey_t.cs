using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveOnKey_t : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // プレイヤー位置の保存
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                PlayerPrefs.SetFloat("PlayerX", playerObj.transform.position.x);
                PlayerPrefs.SetFloat("PlayerY", playerObj.transform.position.y);
            }
            // カメラ位置の保存
            Camera cam = Camera.main;
            if (cam != null)
            {
                PlayerPrefs.SetFloat("CameraX", cam.transform.position.x);
                PlayerPrefs.SetFloat("CameraY", cam.transform.position.y);
            }
            // 死んだ敵のリストを保存
            DeadEnemyData_t.SaveDeadEnemies();
            // 現在のシーン名も保存
            PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            Debug.Log("セーブデータを保存しました！");
        }
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    // プレイヤー位置
        //    PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        //    PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        //    // カメラ位置
        //    Camera cam = Camera.main;
        //    if (cam != null)
        //    {
        //        PlayerPrefs.SetFloat("CameraX", cam.transform.position.x);
        //        PlayerPrefs.SetFloat("CameraY", cam.transform.position.y);
        //    }

        //    // 現在のシーン名も保存
        //    PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
        //    PlayerPrefs.Save();
        //    Debug.Log("セーブデータを保存しました！");
        //}


        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //PlayerPrefs.SetInt("EnemyCount", enemies.Length);
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    PlayerPrefs.SetString("EnemyName" + i, enemies[i].name);
        //}
        //// シーン名
        //PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
        //PlayerPrefs.Save();
        //Debug.Log("セーブ完了！（倒した敵は保存されません＝ロードで復活しません）");

    }
}