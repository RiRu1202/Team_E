using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Gole_n : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("ClearedStage1", 0) == 1)
            {
                PlayerPrefs.SetInt("ClearedStage2", 1);
                PlayerPrefs.SetString("SavedScene", "test_nisimoto");
                PlayerPrefs.Save();
                SceneManager.LoadScene("Clear"); //Clear Ç÷
                Debug.Log("test_nisimoto ÉNÉäÉAÅI");
            }
            else
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
