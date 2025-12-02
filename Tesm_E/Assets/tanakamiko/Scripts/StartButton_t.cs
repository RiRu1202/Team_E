using UnityEngine;
using UnityEngine.SceneManagement;
public class StrtButton_t : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSE;    // Ä¶‚µ‚½‚¢Œø‰Ê‰¹
    public string nextSceneName; // ˆÚ“®æ‚ÌƒV[ƒ“–¼
    public void OnButtonClick()
    {
        StartCoroutine(PlaySEThenChangeScene());
    }
    private System.Collections.IEnumerator PlaySEThenChangeScene()
    {
        PlayerPrefs.DeleteAll();

        audioSource.PlayOneShot(clickSE);
        // Œø‰Ê‰¹‚Ì’·‚³‚¾‚¯‘Ò‚Â
        yield return new WaitForSeconds(clickSE.length);
        SceneManager.LoadScene(nextSceneName);
    }
}
