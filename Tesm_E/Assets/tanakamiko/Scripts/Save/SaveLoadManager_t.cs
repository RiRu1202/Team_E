using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveLoadManager_t : MonoBehaviour
{
    public AudioClip buttonSound;   // 成功時
    public AudioClip errorSound;    // 失敗時（進行不可）

    private AudioSource audioSource;
    private bool isMoving = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    IEnumerator PlaySoundThenLoad(string sceneName)
    {
        isMoving = true;

        if (buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
            yield return new WaitForSeconds(buttonSound.length);
        }

        SceneManager.LoadScene(sceneName);
        isMoving = false;
    }

    void PlayErrorSound()
    {
        if (errorSound != null)
            audioSource.PlayOneShot(errorSound);
    }

    // ============================
    // Continue ボタン
    // ============================
    public void OnContinueButtonPressed()
    {
        if (isMoving) return;

        string saved = PlayerPrefs.GetString("SavedScene", "");

        if (string.IsNullOrEmpty(saved))
        {
            PlayErrorSound();
            Debug.Log("セーブデータがありません");
            return;
        }

        // Stage1未クリア → Stage2に行かせない
        if (saved == "test_nisimoto" && PlayerPrefs.GetInt("ClearedStage1", 0) == 0)
        {
            PlayErrorSound();
            Debug.Log("Stage1未クリアのため Stage2へ進めません");
            return;
        }

        // Stage2未クリア → ラストに行かせない
        if (saved == "Test_tanaka" && PlayerPrefs.GetInt("ClearedStage2", 0) == 0)
        {
            PlayErrorSound();
            Debug.Log("Stage2未クリアのため LastStageへ進めません");
            return;
        }

        //進行可能
        StartCoroutine(PlaySoundThenLoad(saved));
        Debug.Log("Continue → " + saved);
    }

    // ============================
    // 🎯 Next ボタン（Clear 画面）
    // ============================
    public void OnNextSceneButtonPressed()
    {
        if (isMoving) return;

        string now = SceneManager.GetActiveScene().name;

        if (now == "Clear")
        {
            // Stage1クリア済 → Stage2へ
            if (PlayerPrefs.GetInt("ClearedStage1", 0) == 1 &&
                PlayerPrefs.GetInt("ClearedStage2", 0) == 0)
            {
                SaveScene("test_nisimoto");
                StartCoroutine(PlaySoundThenLoad("test_nisimoto"));
                return;
            }

            // Stage2クリア済 → ラストへ
            if (PlayerPrefs.GetInt("ClearedStage2", 0) == 1)
            {
                SaveScene("Test_tanaka");
                StartCoroutine(PlaySoundThenLoad("Test_tanaka"));
                return;
            }

            // 条件外 → エラー音
            PlayErrorSound();
            Debug.Log("進行条件を満たしていません");
            return;
        }

        // Title からは次へ不可
        if (now == "Title")
        {
            PlayErrorSound();
            Debug.Log("タイトル画面では次へは無効です");
        }
    }

    // ============================
    // 保存関数
    // ============================
    public void SaveScene(string sceneName)
    {
        PlayerPrefs.SetString("SavedScene", sceneName);
        PlayerPrefs.Save();
    }
}
