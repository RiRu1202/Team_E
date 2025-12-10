using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Goal_s : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("ClearedStage1", 1);
            PlayerPrefs.SetString("SavedScene", "test_nisimoto");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Clear"); // ⭐ Clear へ
        }
    }
}