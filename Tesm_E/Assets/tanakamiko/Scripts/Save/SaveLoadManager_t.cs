using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveLoadManager_t : MonoBehaviour
{
    // 🔊ボタン効果音
    public AudioClip buttonSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 効果音を鳴らして、長さだけ待ってからシーン移動する処理
    IEnumerator PlaySoundThenLoad(string sceneName)
    {
        audioSource.PlayOneShot(buttonSound);
        yield return new WaitForSeconds(buttonSound.length); // 🎯効果音の長さだけ待つ
        SceneManager.LoadScene(sceneName);
    }

    void PlayButtonSound()
    {
        if (buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }

    // 🎯Continueボタン押したとき
    public void OnContinueButtonPressed()
    {
        if (buttonSound != null)
        {
            string saved = PlayerPrefs.GetString("SavedScene", "");
            if (!string.IsNullOrEmpty(saved))
            {
                StartCoroutine(PlaySoundThenLoad(saved));
            }
            else
            {
                audioSource.PlayOneShot(buttonSound);
                Debug.Log("セーブデータがありません");
            }
        }
    }

    // 🎯「次へボタン」押したとき（クリアシーン内）
    public void OnNextSceneButtonPressed()
    {
        string now = SceneManager.GetActiveScene().name;

        if (now == "Clear")
        {
            // Stage1だけクリア済 -> Stage2へ
            if (PlayerPrefs.GetInt("ClearedStage1", 0) == 1 && PlayerPrefs.GetInt("ClearedStage2", 0) == 0)
            {
                PlayerPrefs.SetString("SavedScene", "Test_sakaguti");
                PlayerPrefs.Save();
                StartCoroutine(PlaySoundThenLoad("Test_sakaguti"));
                return;
            }

            // Stage2クリア済 -> LastStageへ
            if (PlayerPrefs.GetInt("ClearedStage2", 0) == 1)
            {
                PlayerPrefs.SetString("SavedScene", "Test_nisimoto");
                PlayerPrefs.Save();
                StartCoroutine(PlaySoundThenLoad("Test_nisimoto"));
                return;
            }

            // LastStageクリア済ならタイトルへ
            if (PlayerPrefs.GetString("SavedScene", "") == "Test_tanaka")
            {
                PlayerPrefs.SetString("SavedScene", "Title");
                PlayerPrefs.Save();
                StartCoroutine(PlaySoundThenLoad("Title"));
                return;
            }

            // どの条件も合ってない -> タイトル
            StartCoroutine(PlaySoundThenLoad("Title"));
        }

        // 🛑タイトルで次へ押すと何も起きない
        if (now == "Title")
        {
            Debug.Log("タイトル画面では次へは無効です");
            if (buttonSound != null) PlayButtonSound(); // 鳴らしたいなら鳴らせます（※移動はしません）
        }
    }

    // ⭐ステージ移動時だけ保存
    public void SaveScene(string sceneName)
    {
        PlayerPrefs.SetString("SavedScene", sceneName);
        PlayerPrefs.Save();
    }
}
