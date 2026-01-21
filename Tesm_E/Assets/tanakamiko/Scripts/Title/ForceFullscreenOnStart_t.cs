using System.Collections;
using UnityEngine;

public class ForceFullscreenOnStart_t : MonoBehaviour
{
    [Header("安定: FullScreenWindow / 排他: ExclusiveFullscreen")]
    public FullScreenMode mode = FullScreenMode.FullScreenWindow;

    [Header("0ならPCの画面解像度を使う")]
    public int width = 0;
    public int height = 0;

    [Header("他シーンでも維持したいならON")]
    public bool dontDestroyOnLoad = true;

    void Awake()
    {
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start()
    {
        // 起動直後は反映が遅れることがあるので1フレーム待つ
        yield return null;

        int w = width > 0 ? width : Display.main.systemWidth;
        int h = height > 0 ? height : Display.main.systemHeight;

        Screen.fullScreenMode = mode;
        Screen.SetResolution(w, h, mode);
        Screen.fullScreen = true;
    }
}
