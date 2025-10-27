using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RockController_P : MonoBehaviour
{
    public GameObject objPrefab;               // ��������I�u�W�F�N�g
    public float delayTime = 1f;               // �A�˖h�~�p
    public float rockDistance = 3f;            // �v���C���[����̋����i3�}�X��j
    public string targetUIButtonName = "Rock_Frag"; // �N���b�N���肵����UI�̖��O

    private Transform gateTransform;           // �v���C���[�̔��˒n�_
    private float passedTime = 0f;

    void Start()
    {
        gateTransform = transform.Find("playergate");
    }

    void Update()
    {
        if (objPrefab == null || gateTransform == null) return;

        passedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedUI(targetUIButtonName))
            {
                if (passedTime >= delayTime)
                {
                    SpawnObject();
                    passedTime = 0f;
                }
            }
        }
    }

    // �N���b�N����UI�� targetUIButtonName ���`�F�b�N
    private bool IsClickedUI(string targetUIName)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == targetUIName)
                return true;
        }
        return false;
    }

    // �v���C���[��3�}�X��� objPrefab �𐶐�
    private void SpawnObject()
    {
        Vector2 spawnPos = (Vector2)gateTransform.position + (Vector2)gateTransform.right * rockDistance;
        GameObject obj = Instantiate(objPrefab, spawnPos, Quaternion.identity);

        /*// Rigidbody2D�����Ă���Ε����Ŕ�΂��i�C�Ӂj
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            Vector2 dir = gateTransform.right;
            rbody.AddForce(dir * 4f, ForceMode2D.Impulse);
        }*/
    }
}
