using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal_t : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stage2クリア済みならラストゴールを認める
            if (PlayerPrefs.GetInt("ClearedStage2", 0) == 1)
            {
                // 最後にクリアしたステージとして保存
                PlayerPrefs.SetString("SavedScene", "Test_tanaka");
                PlayerPrefs.Save();

                // ⭐ ラスト専用のクリア画面へ行く
                SceneManager.LoadScene("LastClear");

                Debug.Log("Test_tanaka クリア！ → LastClearへ");
            }
            else
            {
                // Stage2クリアしていないのに来たらタイトルへ
                SceneManager.LoadScene("Title");
            }
        }
    }
}