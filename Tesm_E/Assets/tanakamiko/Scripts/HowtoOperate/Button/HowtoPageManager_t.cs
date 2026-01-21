using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class HowtoPageManager_t : MonoBehaviour
{

    [Header("ページ一覧（順番は自由）")]
    public List<GameObject> pages = new List<GameObject>();

    Stack<GameObject> history = new Stack<GameObject>();
    GameObject current;

    void Start()
    {
        // 最初にONになっているページを現在ページにする
        foreach (var p in pages)
        {
            if (p != null && p.activeSelf)
            {
                current = p;
                break;
            }
        }
    }

    // ★次のページへ（呼び出し側で指定）
    public void Show(GameObject nextPage)
    {
        if (nextPage == null) return;
        if (current == nextPage) return;

        if (current != null)
        {
            history.Push(current);
            current.SetActive(false);
        }

        nextPage.SetActive(true);
        current = nextPage;
    }

    // ★前のページへ
    public void Back()
    {
        if (history.Count == 0) return;

        if (current != null)
            current.SetActive(false);

        GameObject prev = history.Pop();
        prev.SetActive(true);
        current = prev;
    }

    public void ShowAfterDelay(GameObject nextPage, float delay)
    {
        StartCoroutine(ShowAfterDelayRoutine(nextPage, delay));
    }

    IEnumerator ShowAfterDelayRoutine(GameObject nextPage, float delay)
    {
        yield return new WaitForSeconds(delay);
        Show(nextPage);
    }

}
