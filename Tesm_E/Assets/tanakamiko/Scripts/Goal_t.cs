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
                PlayerPrefs.SetString("SavedScene", "Test_tanaka");
                PlayerPrefs.Save();
                SceneManager.LoadScene("Clear"); // ⭐ クリアシーン共通へ
                Debug.Log("Test_tanaka クリア！");
            }
            else
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}