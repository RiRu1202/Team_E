using System.Collections;
using UnityEngine;

public class TitleQuitButton_t : MonoBehaviour
{
    [Header("クリック音")]
    public AudioSource seSource;     // 2DのAudioSource推奨
    public AudioClip clickSE;        // 鳴らしたいSE

    [Header("SEを鳴らしてから終了する秒数（0ならクリップ長）")]
    public float delay = 0f;

    // UI ButtonのOnClickに入れる
    public void QuitGame()
    {
        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        // SE再生
        float wait = 0f;
        if (seSource != null && clickSE != null)
        {
            seSource.PlayOneShot(clickSE);

            wait = (delay > 0f) ? delay : clickSE.length;
        }
        else
        {
            // 設定漏れでも即終了はできるようにする
            wait = (delay > 0f) ? delay : 0f;
        }

        if (wait > 0f) yield return new WaitForSeconds(wait);

        // 終了（Editorとビルドで処理を分ける）
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

