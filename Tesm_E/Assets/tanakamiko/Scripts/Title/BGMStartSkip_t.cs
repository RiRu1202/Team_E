using UnityEngine;

public class BGMStartSkip_t : MonoBehaviour
{
    public AudioSource bgmSource;
    public float skipSeconds = 0.5f; // –³‰¹‚Ì’·‚³‚¾‚¯‘‘—‚èi—á: 0.5•bj

    void Start()
    {
        bgmSource.time = skipSeconds;
        bgmSource.Play();
    }
}

