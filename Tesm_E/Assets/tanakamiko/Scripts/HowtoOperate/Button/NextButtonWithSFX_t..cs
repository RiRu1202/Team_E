using UnityEngine;

public class NextButtonWithSFX_t : MonoBehaviour
{
    public HowtoPageManager_t pageManager;
    public GameObject nextPage;

    [Header("Œø‰Ê‰¹")]
    public AudioSource seSource;
    public AudioClip clickSE;

    public float delay = 0.1f;

    public void Next()
    {
        if (seSource != null && clickSE != null)
            seSource.PlayOneShot(clickSE);

        if (pageManager == null || nextPage == null) return;

        // š©•ª‚Å‚Í‚È‚­AÁ‚¦‚È‚¢PageManager‚Å’x‰„Às‚·‚é
        pageManager.ShowAfterDelay(nextPage, delay);
    }
}
