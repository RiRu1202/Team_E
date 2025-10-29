using UnityEngine;

/// <summary>
/// 2D �v���C���[�̈ړ��ƃW�����v�𐧌䂷��X�N���v�g
/// </summary>
public class PlayerController_p : MonoBehaviour
{
    [Header("�v���C���[�̈ړ��ݒ�")]
    public float moveSpeed = 5f;   // ���ړ��̑���
    public float jumpForce = 5f;   // �W�����v�́i������ւ̗́j

    private Rigidbody2D rb;        // �v���C���[�� Rigidbody2D �R���|�[�l���g�i�������Z�Ɏg�p�j
    private bool isGrounded = false; // �n�ʂɐڂ��Ă��邩�ǂ����i�W�����v����Ɏg�p�j

    // �Q�[���J�n���Ɉ�x�����Ă΂�郁�\�b�h
    void Start()
    {
        // �t���[�����[�g��60FPS�ɌŒ�
        Application.targetFrameRate = 60;

        // Rigidbody2D �R���|�[�l���g���擾���ăL���b�V��
        rb = GetComponent<Rigidbody2D>();
    }

    // ���t���[���Ă΂�郁�\�b�h
    void Update()
    {
        // �X�y�[�X�L�[��������A���n�ʂɐڂ��Ă���ꍇ�̂݃W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // ������ɗ͂������ăW�����v
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // �����ƏՓ˂����u�ԂɌĂ΂��i2D �����j
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g���uground�v���C���[�������ꍇ�A�n�ʂɐڒn���Ă���Ɣ���
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
        }
    }

    // �Փ˂��Ă����I�u�W�F�N�g���痣�ꂽ�u�ԂɌĂ΂��i2D �����j
    void OnCollisionExit2D(Collision2D collision)
    {
        // ���ꂽ�I�u�W�F�N�g���uground�v���C���[�������ꍇ�A�ڒn���Ă��Ȃ��Ɣ���
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = false;
        }
    }
}
