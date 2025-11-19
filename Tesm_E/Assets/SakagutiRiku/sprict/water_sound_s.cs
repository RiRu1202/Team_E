using System.Collections;
using UnityEngine;

public class water_sound_s : MonoBehaviour
{
    [Header("クリック時に鳴らす音")]
    public AudioClip sound1;

    [Header("クールタイム（秒）")]
    public float delayTime = 2f;

    private AudioSource audioSource;
    private bool isCoolingDown = false;

    void Start()
    {
        // AudioSource コンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource コンポーネントがこのオブジェクトにありません！");
        }

        if (sound1 == null)
        {
            Debug.LogWarning("AudioClip（sound1）が設定されていません。");
        }
    }

    void Update()
    {
        // 左クリックを検知
        if (Input.GetMouseButtonDown(0))
        {
            // マウス位置をワールド座標に変換
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 2D用のレイキャスト（Z方向ではなくXY平面）
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log($"クリック対象: {hit.collider.name}");

                // Water_Fragタグがついているオブジェクトなら
                if (hit.collider.CompareTag("Water_Frag"))
                {
                    TryPlaySound();
                }
            }
            else
            {
                Debug.Log("何もクリックされていません。");
            }
        }
    }

    void TryPlaySound()
    {
        // クールタイム中はスキップ
        if (isCoolingDown)
        {
            Debug.Log("クールタイム中です。");
            return;
        }

        // 音を鳴らす
        if (audioSource != null && sound1 != null)
        {
            Debug.Log("音を再生します。");
            audioSource.PlayOneShot(sound1);
        }

        // クールタイム開始
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(delayTime);
        isCoolingDown = false;
        Debug.Log("クールタイム終了");
    }
}
