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
        // ★ セーブデータを破棄
        ClearSaveData();

        StartCoroutine(QuitRoutine());
    }

    void ClearSaveData()
    {
        // 全セーブ削除（進行・SavedSceneなど全部）
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("セーブデータをすべて削除しました");
    }

    IEnumerator QuitRoutine()
    {
        float wait = 0f;

        // SE再生
        if (seSource != null && clickSE != null)
        {
            seSource.PlayOneShot(clickSE);
            wait = (delay > 0f) ? delay : clickSE.length;
        }
        else
        {
            wait = (delay > 0f) ? delay : 0f;
        }

        if (wait > 0f)
            yield return new WaitForSeconds(wait);

        // 終了（Editorとビルドで分岐）
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}


