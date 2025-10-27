using UnityEngine;

    public class Soil : MonoBehaviour
    {
        public Transform player; // キャラクターのTransform
        public float collapseDistance = 5f; // 崩壊する距離
        public GameObject collapseEffect; // 崩壊エフェクト（Particle Systemなど）

        private bool isCollapsed = false;

        void Update()
        {
            // キャラクターとの距離を計算
            float distance = Vector2.Distance(transform.position, player.position);

            // 一定距離を超えたら崩壊
            if (distance > collapseDistance && !isCollapsed)
            {
                Collapse();
            }
        }

        void Collapse()
        {
            isCollapsed = true;

            // 崩壊エフェクトを生成
            if (collapseEffect != null)
            {
                Instantiate(collapseEffect, transform.position, Quaternion.identity);
            }

            // ブロックを削除
            Destroy(gameObject);
        }
    }
