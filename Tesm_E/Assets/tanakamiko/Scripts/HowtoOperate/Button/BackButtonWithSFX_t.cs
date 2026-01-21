using System.Collections;
using UnityEngine;

public class BackButtonWithSFX_t : MonoBehaviour
{
    public HowtoPageManager_t pageManager;

    [Header("Œø‰Ê‰¹")]
    public AudioSource seSource;
    public AudioClip clickSE;

    [Header("Ø‚è‘Ö‚¦’x‰„iSE‚ğ–Â‚ç‚µ‚Ä‚©‚ç–ß‚éj")]
    public float delay = 0.1f;

    public void Back()
    {
        StartCoroutine(BackRoutine());
    }

    IEnumerator BackRoutine()
    {
        if (seSource != null && clickSE != null)
            seSource.PlayOneShot(clickSE);

        yield return new WaitForSeconds(delay);

        if (pageManager != null)
            pageManager.Back();
    }
}

