using UnityEngine;
using System.Collections;
public class TreeGrow_t : MonoBehaviour
{
    public float growHeight = 3.5f;   // 最終サイズ（3.5マス）
    public float growTime = 0.5f;     // 成長にかかる時間
    public float treeWidth = 2f;      //横幅
    void Start()
    {
        StartCoroutine(Grow());
    }
    IEnumerator Grow()
    {
        Vector3 startScale = new Vector3(treeWidth, 0.1f, 1); // 最初は小さく
        Vector3 endScale = new Vector3(treeWidth, growHeight, 1);
        float time = 0;
        transform.localScale = startScale;
        while (time < growTime)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, time / growTime);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }
}