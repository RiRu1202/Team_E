using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Burst.Intrinsics.X86;


public class Howtooperate_t : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSE;    // 再生したい効果音
    public string nextSceneName; // 移動先のシーン名
    public void OnButtonClick()
    {
        StartCoroutine(PlaySEThenChangeScene());
    }

    //他のシーンから呼び出せるように　public
    private System.Collections.IEnumerator PlaySEThenChangeScene()
    {
        audioSource.PlayOneShot(clickSE);
        // 効果音の長さだけ待つ
        yield return new WaitForSeconds(clickSE.length);
        SceneManager.LoadScene(nextSceneName);
    }
}
