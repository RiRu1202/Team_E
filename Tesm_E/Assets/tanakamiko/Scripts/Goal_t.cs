using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal_t : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Clear"); // ← クリアシーン名を入れる
        }
    }
}