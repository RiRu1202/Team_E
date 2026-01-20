using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal_t : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Player"))
            {
                if (PlayerPrefs.GetInt("ClearedStage2", 0) == 1)
                {
                    // ⭐ 次がないので Title を保存
                    PlayerPrefs.SetString("SavedScene", "Title");
                    PlayerPrefs.Save();

                    // 次へボタンなしのクリア画面へ
                    SceneManager.LoadScene("LastClear");
                }
                else
                {
                    SceneManager.LoadScene("Title");
                }
            }
    }
}