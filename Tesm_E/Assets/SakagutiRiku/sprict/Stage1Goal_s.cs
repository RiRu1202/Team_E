using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Goal_s : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("ClearedStage1", 1);
            PlayerPrefs.SetString("SavedScene", "Test_sakagiti");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Clear"); // ⭐ Clear へ
            Debug.Log("Test_sakagiti クリア！");
        }
    }
}