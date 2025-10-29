using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 3.0f;  // �J�����̃X�N���[�����x
    public Transform player;          // �v���C���[
    public Transform goal;            // �S�[��
    public static string gameState = "playing";  // "playing" / "clear" / "gameover"

    void Start()
    {
        Application.targetFrameRate = 60;
        gameState = "playing";
    }

    void FixedUpdate()
    {
        // �Q�[�����łȂ���Ή������Ȃ�
        if (gameState != "playing") return;

        // �v���C���[����������Q�[���I�[�o�[�i��F���S�ȂǂŔj�����ꂽ�ꍇ�j
        if (player == null)
        {
            gameState = "gameover";
            return;
        }

        // �v���C���[���S�[���ɓ��B������N���A
        if (player.position.x >= goal.position.x)
        {
            gameState = "clear";
            return;
        }

        // �J�������E�ɃX�N���[��
        transform.position += new Vector3(scrollSpeed * Time.fixedDeltaTime, 0, 0);
    }
}
