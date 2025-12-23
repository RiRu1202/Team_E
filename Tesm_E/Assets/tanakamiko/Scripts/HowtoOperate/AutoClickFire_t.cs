using System.Collections;
using UnityEngine;

public class AutoClickFire_t : MonoBehaviour
{
    public FireController_Demo_t fireController;

    [Header("‘ÎÛ")]
    public GameObject oniDemo;   // •œŠˆ‚³‚¹‚é‹S

    [Header("”­ËŠÔŠu")]
    public float firstDelay = 2.0f;
    public float interval = 10f;

    [Header("DƒTƒCƒY")]
    public Vector3 normalScale = new Vector3(0.5f, 0.5f, 1f);
    public Vector3 pressedScale = new Vector3(0.45f, 0.45f, 1f);
    public float pressDuration = 0.1f;

    Coroutine loop;
    Transform cardTransform;

    void Awake()
    {
        cardTransform = transform;
        cardTransform.localScale = normalScale;
    }

    void OnEnable()
    {
        loop = StartCoroutine(AutoFireLoop());
    }

    void OnDisable()
    {
        if (loop != null)
        {
            StopCoroutine(loop);
            loop = null;
        }

        cardTransform.localScale = normalScale;
    }

    IEnumerator AutoFireLoop()
    {
        // Å‰‚Ì1‰ñ‚¾‚¯‘Ò‚Â
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            // D‚Ì‰Ÿ‚µ‚İ‰‰o
            yield return StartCoroutine(PressAnimation());

            // ‰Š‚ğ”­Ë
            fireController.Fire();

            // š‹S‚ğ‘¦•œŠˆiÁ‚¦‚½’¼Œãj
            if (oniDemo != null)
            {
                oniDemo.SetActive(true);
            }

            // Ÿ‚Ì”­Ë‚Ü‚Å‘Ò‚Â
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator PressAnimation()
    {
        cardTransform.localScale = pressedScale;
        yield return new WaitForSeconds(pressDuration);
        cardTransform.localScale = normalScale;
    }
}

    //public FireController_Demo_t fireController;

    //[Header("‘ÎÛ")]
    //public GameObject oniDemo;   // š•œŠˆ‚³‚¹‚é‹S

    //[Header("”­ËŠÔŠu")]
    //public float firstDelay = 2.0f;
    //public float interval = 10f;

    //[Header("DƒTƒCƒY")]
    //public Vector3 normalScale = new Vector3(0.5f, 0.5f, 1f);
    //public Vector3 pressedScale = new Vector3(0.45f, 0.45f, 1f);
    //public float pressDuration = 0.1f;

    //Coroutine loop;
    //Transform cardTransform;

    //void Awake()
    //{
    //    cardTransform = transform;
    //    cardTransform.localScale = normalScale;
    //}

    //void OnEnable()
    //{
    //    loop = StartCoroutine(AutoFireLoop());
    //}

    //void OnDisable()
    //{
    //    if (loop != null)
    //    {
    //        StopCoroutine(loop);
    //        loop = null;
    //    }

    //    cardTransform.localScale = normalScale;
    //}

    //IEnumerator AutoFireLoop()
    //{
    //    // Å‰‚Ì1‰ñ‚¾‚¯‘Ò‚Â
    //    yield return new WaitForSeconds(firstDelay);

    //    while (true)
    //    {
    //        // š‹S‚ğ•K‚¸•œŠˆ‚³‚¹‚é
    //        if (oniDemo != null)
    //        {
    //            oniDemo.SetActive(true);
    //        }

    //        // D‚Ì‰Ÿ‚µ‚İ‰‰o
    //        yield return StartCoroutine(PressAnimation());

    //        // ‰Š‚ğ”­Ë
    //        fireController.Fire();

    //        yield return new WaitForSeconds(interval);
    //    }
    //}

    //IEnumerator PressAnimation()
    //{
    //    cardTransform.localScale = pressedScale;
    //    yield return new WaitForSeconds(pressDuration);
    //    cardTransform.localScale = normalScale;
    //}


