using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RockController : MonoBehaviour
{
    public GameObject rockPrefab;
    public Vector2 checkBoxSize = new Vector2(1f, 1f);
    public LayerMask blockLayer;
    public float shiftDistance = 1.0f;
    public string targetObjectName = "RockButton"; // �N���b�N�Ώۂ̖��O
    public string playerGateName = "PlayerGate";   // ������̃I�u�W�F�N�g��
    public float spawnOffset = 3f; // PlayerGate����E��3�}�X

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedTarget(targetObjectName))
            {
                GameObject gate = GameObject.Find(playerGateName);
                if (gate != null)
                {
                    Vector2 spawnPos = (Vector2)gate.transform.position + Vector2.right * spawnOffset;
                    TrySpawnRock(spawnPos);
                }
                else
                {
                    Debug.LogError("PlayerGate��������܂���I");
                }
            }
        }
    }

    // �N���b�N�����I�u�W�F�N�g�� targetName ���ǂ�������
    private bool IsClickedTarget(string targetName)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        foreach (RaycastResult r in results)
        {
            if (r.gameObject.name == targetName)
                return true;
        }

        return false;
    }

    // �w��ʒu�����𐶐��A�u���b�N������΍��ɂ��炷
    public void TrySpawnRock(Vector2 startPos)
    {
        Vector2 pos = startPos;
        int maxTries = 5;

        for (int i = 0; i < maxTries; i++)
        {
            Collider2D hit = Physics2D.OverlapBox(pos, checkBoxSize, 0f, blockLayer);
            if (hit == null)
            {
                Instantiate(rockPrefab, pos, Quaternion.identity);
                Debug.Log($"��� {i} �񍶂ɂ��炵�Đ������܂����B�ʒu: {pos}");
                return;
            }

            pos += Vector2.left * shiftDistance;
        }

        Debug.LogWarning("�󂢂Ă���ꏊ��������܂���ł����B��𐶐����܂���B");
    }

    // Scene�r���[�Ŕ���͈͂�������悤�ɂ���
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        GameObject gate = GameObject.Find(playerGateName);
        if (gate != null)
            Gizmos.DrawWireCube((Vector2)gate.transform.position + Vector2.right * spawnOffset, checkBoxSize);
    }
}
