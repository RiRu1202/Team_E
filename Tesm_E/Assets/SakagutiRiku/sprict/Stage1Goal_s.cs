using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Goal_t : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("ClearedStage1", 1);
            //FindObjectOfType<SaveLoadManager_t>().SaveScene("Stage1");
            SceneManager.LoadScene("StageClear");
        }
    }
}