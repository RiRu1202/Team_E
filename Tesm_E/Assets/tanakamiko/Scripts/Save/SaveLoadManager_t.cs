using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveLoadManager_t : MonoBehaviour
{
    //ボタン効果音
    public AudioClip buttonSound;
    private AudioSource audioSource;
    private bool isMoving = false; // 連打防止ロック

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 効果音→待機→シーン移動コルーチン
    IEnumerator PlaySoundThenLoad(string sceneName)
    {
        isMoving = true;
        isMoving = true;
        if (buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
            yield return new WaitForSeconds(buttonSound.length);
        }
        SceneManager.LoadScene(sceneName);
        isMoving = false;
    }

    // =========================
    // Continueボタン処理
    // =========================
    public void OnContinueButtonPressed()
    {
        if (isMoving) return;

        string savedScene = PlayerPrefs.GetString("SavedScene", "");
        if (!string.IsNullOrEmpty(savedScene))
        {
            StartCoroutine(PlaySoundThenLoad(savedScene));
            Debug.Log("Continue → " + savedScene + " から再開");
        }
        else
        {
            if (buttonSound != null) audioSource.PlayOneShot(buttonSound);
            Debug.Log("セーブデータがありません");
        }
    }

    // =========================
    // 次のシーンへボタン処理
    // =========================
    public void OnNextSceneButtonPressed()
    {
        if (isMoving) return;

        string now = SceneManager.GetActiveScene().name;

        if (now == "Clear")
        {
            //Stage1だけクリア済 → Stage2へ
            if (PlayerPrefs.GetInt("ClearedStage1", 0) == 1 &&
                PlayerPrefs.GetInt("ClearedStage2", 0) == 0)
            {
                SaveScene("test_nisimoto"); // 次の復帰先を保存
                StartCoroutine(PlaySoundThenLoad("test_nisimoto"));
                Debug.Log("Clear → test_nisimoto へ");
                return;
            }

            //Stage2もクリア済 → ラストステージへ
            if (PlayerPrefs.GetInt("ClearedStage2", 0) == 1)
            {
                SaveScene("Test_tanaka");
                StartCoroutine(PlaySoundThenLoad("Test_tanaka"));
                Debug.Log("Clear → Test_tanaka へ");
                return;
            }

            //ラストクリア済 → タイトルへ戻る
            if (PlayerPrefs.GetString("SavedScene", "") == "Test_tanaka")
            {
                SaveScene("Title");
                StartCoroutine(PlaySoundThenLoad("Title"));
                Debug.Log("Clear → Title へ戻る");
                return;
            }

            // どの条件에도合わない → タイトルへ
            StartCoroutine(PlaySoundThenLoad("Title"));
            return;
        }

        //タイトルで Next 押す → 何も起きない
        if (now == "Title")
        {
            Debug.Log("タイトル画面では次へは無効です");
        }
    }

    // =========================
    //シーン名だけ保存
    // =========================
    public void SaveScene(string sceneName)
    {
        PlayerPrefs.SetString("SavedScene", sceneName);
        PlayerPrefs.Save();
    }
}
