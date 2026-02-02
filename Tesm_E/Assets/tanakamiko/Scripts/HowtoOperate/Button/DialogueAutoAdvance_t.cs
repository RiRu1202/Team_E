using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueAutoAdvance_t : MonoBehaviour
{
    [Header("表示先")]
    public TextMeshProUGUI dialogueText;

    [Header("セリフ内容")]
    [TextArea(2, 4)]
    public string[] lines;

    [Header("文字送り速度")]
    public float charInterval = 0.03f;

    [Header("自動送りまでの待ち時間（秒）")]
    public float autoNextDelay = 4.0f;

    int index = 0;
    Coroutine playRoutine;

    void OnEnable()
    {
        index = 0;
        if (playRoutine != null)
            StopCoroutine(playRoutine);

        playRoutine = StartCoroutine(PlayDialogue());
    }

    void OnDisable()
    {
        if (playRoutine != null)
        {
            StopCoroutine(playRoutine);
            playRoutine = null;
        }
    }

    IEnumerator PlayDialogue()
    {
        while (index < lines.Length)
        {
            // ① セリフをタイプ表示
            yield return StartCoroutine(TypeLine(lines[index]));

            // ② 表示完了後、4秒待つ
            yield return new WaitForSeconds(autoNextDelay);

            index++;
        }

        // ★ここで最後まで終わった
        // 次へボタンを出したいなら、ここで SetActive(true) など
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";

        for (int i = 0; i < line.Length; i++)
        {
            dialogueText.text += line[i];
            yield return new WaitForSeconds(charInterval);
        }
    }
}


