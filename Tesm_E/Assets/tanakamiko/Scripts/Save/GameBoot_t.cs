using UnityEngine;

public class GameBoot_t : MonoBehaviour
{
    void Awake()
    {
        // 前回が異常終了ならセーブ破棄
        if (PlayerPrefs.GetInt("IsRunning", 0) == 1)
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("前回は異常終了 → セーブ削除");
        }

        // 起動中フラグを立てる
        PlayerPrefs.SetInt("IsRunning", 1);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        // 正常終了時はフラグを下ろす
        PlayerPrefs.SetInt("IsRunning", 0);
        PlayerPrefs.Save();
    }
}
