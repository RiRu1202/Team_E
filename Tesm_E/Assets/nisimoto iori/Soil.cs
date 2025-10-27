using UnityEngine;

    public class Soil : MonoBehaviour
    {
        public Transform player; // �L�����N�^�[��Transform
        public float collapseDistance = 5f; // ���󂷂鋗��
        public GameObject collapseEffect; // ����G�t�F�N�g�iParticle System�Ȃǁj

        private bool isCollapsed = false;

        void Update()
        {
            // �L�����N�^�[�Ƃ̋������v�Z
            float distance = Vector2.Distance(transform.position, player.position);

            // ��苗���𒴂��������
            if (distance > collapseDistance && !isCollapsed)
            {
                Collapse();
            }
        }

        void Collapse()
        {
            isCollapsed = true;

            // ����G�t�F�N�g�𐶐�
            if (collapseEffect != null)
            {
                Instantiate(collapseEffect, transform.position, Quaternion.identity);
            }

            // �u���b�N���폜
            Destroy(gameObject);
        }
    }
