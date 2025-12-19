using UnityEngine;

public class stagestartsave_s : MonoBehaviour
{
    void Start()
    {
        // ステージに入った時点で「最初から再開」用に保存
        PlayerPrefs.SetString("SavedScene", "Test_sakagiti");
        PlayerPrefs.Save();
    }
}

