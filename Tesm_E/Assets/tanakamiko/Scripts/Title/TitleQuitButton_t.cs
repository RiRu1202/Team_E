using System.Collections;
using UnityEngine;

public class TitleQuitButton_t : MonoBehaviour
{
    [Header("クリック音")]
    public AudioSource seSource;
    public AudioClip clickSE;

    [Header("SEを鳴らしてから終了する秒数（0ならクリップ長）")]
    public float delay = 0f;

    public void QuitGame()
    {
        // ★ ゲーム終了時のみセーブ削除
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        float wait = 0f;

        if (seSource != null && clickSE != null)
        {
            seSource.PlayOneShot(clickSE);
            wait = (delay > 0f) ? delay : clickSE.length;
        }

        if (wait > 0f)
            yield return new WaitForSeconds(wait);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
