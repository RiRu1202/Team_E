using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �}�E�X�N���b�N�œ����UI�{�^���i��FFire_Frag�j�������ꂽ�Ƃ��A
/// �v���C���[�́u���ˌ��iplayergate�j�v����e�iobjPrefab�j�𔭎˂���X�N���v�g�B
/// </summary>
public class FireController_P : MonoBehaviour
{
    [Header("���ːݒ�")]
    public GameObject objPrefab;         // ���˂���I�u�W�F�N�g�i��F�e�A�΋��Ȃǁj�̃v���n�u
    public float delayTime = 1f;         // ���ˊԊu�i�b�j
    public float fireSpeed = 4.0f;       // �e���΂����x�i�͂̑傫���j

    [Header("UI�ݒ�")]
    public string targetUIButtonName = "Fire_Frag"; // ���˂��g���K�[����UI�{�^���̖��O

    private Transform gateTransform;     // �e�̔��ˈʒu�i�q�I�u�W�F�N�g "playergate" ��Transform�j
    private float passedTime = 0f;       // �O��̔��˂���o�߂�������

    // �Q�[���J�n����1�x�����Ă΂��
    void Start()
    {
        // �q�I�u�W�F�N�g "playergate" ��T���ăL���b�V��
        gateTransform = transform.Find("playergate");
    }

    // ���t���[���Ă΂��
    void Update()
    {
        // ���˂���I�u�W�F�N�g�i�v���n�u�j���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (objPrefab == null) return;

        // �o�ߎ��Ԃ����Z�i���ˊԊu�̊Ǘ��Ɏg�p�j
        passedTime += Time.deltaTime;

        // �}�E�X�̍��N���b�N�������ꂽ�u��
        if (Input.GetMouseButtonDown(0))
        {
            // �w�肵��UI�itargetUIButtonName�j���N���b�N���ꂽ���`�F�b�N
            if (IsClickedUI(targetUIButtonName))
            {
                // ���̔��ˊԊu�idelayTime�j���o�߂��Ă����甭��
                if (passedTime >= delayTime)
                {
                    Fire();          // �e�𔭎�
                    passedTime = 0f; // �o�ߎ��Ԃ����Z�b�g
                }
            }
        }
    }

    /// <summary>
    /// �w�肵��UI�v�f���N���b�N���ꂽ���ǂ����𔻒肷�郁�\�b�h
    /// </summary>
    private bool IsClickedUI(string targetUIName)
    {
        // ���݂̃}�E�X�ʒu����UI��̃N���b�N������쐬
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        // �N���b�N�ʒu�ɂ���S�Ă�UI�v�f���擾
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // �N���b�N���ꂽUI�̒��Ɏw��̖��O�����邩�`�F�b�N
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == targetUIName)
            {
                return true; // �w�肵��UI���N���b�N����Ă���
            }
        }

        return false; // �Y��UI���N���b�N����Ă��Ȃ�
    }

    /// <summary>
    /// �e�iobjPrefab�j�𐶐����āA�O�����iplayergate �̉E�����j�ɔ��˂���
    /// </summary>
    public void Fire()
    {
        // ���ˈʒu���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (gateTransform == null) return;

        // �e�̐����ʒu���擾�iplayergate �̈ʒu�j
        Vector2 pos = gateTransform.position;

        // �e�v���n�u�𐶐�
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

        // �e�� Rigidbody2D �����Ă���Η͂������Ĕ���
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            // playergate �́u�E�����v�Ɍ������Ĕ���
            Vector2 dir = gateTransform.right;
            rbody.AddForce(dir * fireSpeed, ForceMode2D.Impulse);
        }
    }
}
