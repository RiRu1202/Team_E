using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueEnter_t : MonoBehaviour
{
    [Header("表示先")]
    public TextMeshProUGUI dialogueText;

    [Header("セリフ内容（ここに順番に書く）")]
    [TextArea(2, 4)]
    public string[] lines;

    [Header("タイプ速度（0.02〜0.05が見やすい）")]
    public float charInterval = 0.03f;

    [Header("Enterキー")]
    public KeyCode nextKey = KeyCode.Return;

    int index = 0;
    Coroutine typingRoutine;
    bool isTyping = false;

    void OnEnable()
    {
        index = 0;
        StopTyping();
        ShowLine(index);
    }

    void Update()
    {
        if (dialogueText == null) return;
        if (lines == null || lines.Length == 0) return;

        if (Input.GetKeyDown(nextKey))
        {
            // 文字が出てる途中なら、全部表示して止める
            if (isTyping)
            {
                StopTyping();
                dialogueText.text = lines[index];
                return;
            }

            // 次の行へ
            index++;

            // 最後を超えたら何もしない（必要ならここで次へボタン表示にしてもOK）
            if (index >= lines.Length)
            {
                index = lines.Length - 1;
                return;
            }

            ShowLine(index);
        }
    }

    void ShowLine(int i)
    {
        if (dialogueText == null) return;
        StopTyping();
        typingRoutine = StartCoroutine(TypeLine(lines[i]));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        for (int c = 0; c < line.Length; c++)
        {
            dialogueText.text += line[c];
            yield return new WaitForSeconds(charInterval);
        }

        isTyping = false;
    }

    void StopTyping()
    {
        if (typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
            typingRoutine = null;
        }
        isTyping = false;
    }
}

