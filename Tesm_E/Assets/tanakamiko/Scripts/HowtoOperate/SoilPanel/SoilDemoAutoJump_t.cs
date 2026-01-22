using System.Collections;
using UnityEngine;

public class SoilDemoAutoJump_t : MonoBehaviour
{
    [Header("ƒWƒƒƒ“ƒvİ’è")]
    public float jumpForce = 5f;

    [Header("QÆ")]
    public PlayerScrole_t playerMove;
    public CameraController_t cameraCtrl;
    public AutoClickSoilDemo_t soilAuto;     // ‘–‚èo‚µ‚Ä1•bŒã‚É“y
    public Transform soilSpawnedRoot;        // ¶¬‚µ‚½“y‚¾‚¯‚ª“ü‚éeiÁ‚µ‚ÄOK‚È•ûj

    [Header("ƒŠƒZƒbƒgˆÊ’u")]
    public Transform resetPlayerPos;
    public Transform resetCameraPos;

    [Header("ƒ‹[ƒv‘Ò‚¿")]
    public float loopWaitMin = 1f;
    public float loopWaitMax = 2f;

    Rigidbody2D rb;
    bool resetting = false;
    Coroutine resetRoutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // šƒpƒlƒ‹‚É–ß‚Á‚Ä‚«‚½‚Ég“r’†ó‘Ôh‚ğƒŠƒZƒbƒg
        resetting = false;

        if (rb != null) rb.simulated = true;
        if (playerMove != null) playerMove.enabled = true;
        if (cameraCtrl != null) cameraCtrl.isPaused = false;

        // “y‚ğÅ‰‚©‚çn‚ß‚½‚¢‚È‚çi”CˆÓj
        if (soilAuto != null) soilAuto.StartCycle();
    }

    void OnDisable()
    {
        // š“r’†‚ÅŸ‚Ö/–ß‚é‚ğ‰Ÿ‚³‚ê‚½‚ÌgŒãn––h
        if (resetRoutine != null)
        {
            StopCoroutine(resetRoutine);
            resetRoutine = null;
        }

        resetting = false;

        if (rb != null)
        {
            rb.simulated = true;          // OFF‚Ì‚Ü‚Üc‚³‚È‚¢
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (playerMove != null) playerMove.enabled = true;  // OFF‚Ì‚Ü‚Üc‚³‚È‚¢
        if (cameraCtrl != null) cameraCtrl.isPaused = true; // ƒpƒlƒ‹ŠO‚Å‚Í~‚ß‚é

        // š¶¬‚µ‚½“y‚ğÁ‚·i”CˆÓFŸ‰ñ‚Ì×–‚–h~j
        if (soilSpawnedRoot != null)
        {
            for (int i = soilSpawnedRoot.childCount - 1; i >= 0; i--)
                Destroy(soilSpawnedRoot.GetChild(i).gameObject);
        }
    }

    void DoJump()
    {
        if (rb == null) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DemoJump"))
        {
            DoJump();
        }

        if (other.CompareTag("DemoGoal") && !resetting)
        {
            resetting = true;
            resetRoutine = StartCoroutine(ResetLoop());
        }
    }

    IEnumerator ResetLoop()
    {
        // ‡@~‚ß‚é
        if (playerMove != null) playerMove.enabled = false;
        if (cameraCtrl != null) cameraCtrl.isPaused = true;
        if (rb != null) rb.simulated = false;

        // ‡A“y‚ğÁ‚·iŸü‰ñ‚Ì×–‚–h~j
        if (soilSpawnedRoot != null)
        {
            for (int i = soilSpawnedRoot.childCount - 1; i >= 0; i--)
                Destroy(soilSpawnedRoot.GetChild(i).gameObject);
        }

        // ‡B‘Ò‚Â
        float wait = Random.Range(loopWaitMin, loopWaitMax);
        yield return new WaitForSeconds(wait);

        // ‡CˆÊ’u–ß‚µ
        if (resetPlayerPos != null) transform.position = resetPlayerPos.position;

        if (resetCameraPos != null)
        {
            Vector3 p = resetCameraPos.position;
            p.z = -10f;
            Camera.main.transform.position = p;
        }

        // ‡D•œ‹A
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = true;
        }

        yield return null;

        if (playerMove != null) playerMove.enabled = true;
        if (cameraCtrl != null) cameraCtrl.isPaused = false;

        // šü‰ñŠJnF“y‚ğ‚Ü‚½1•bŒã‚Éo‚·
        if (soilAuto != null) soilAuto.StartCycle();

        resetting = false;
        resetRoutine = null;
    }
}

