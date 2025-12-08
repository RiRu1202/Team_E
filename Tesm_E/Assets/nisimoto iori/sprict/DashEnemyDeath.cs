<<<<<<< HEAD
・ｿusing UnityEngine;
 public class DashEnemyDeath : MonoBehaviour
{[Header("遘ｻ蜍戊ｨｭ螳・)] public float speed = 2f; // 蟾ｦ縺ｫ遘ｻ蜍輔☆繧九せ繝斐・繝・
private bool isDead = false; // 謨ｵ縺悟偵＆繧後◆縺九←縺・°縺ｮ繝輔Λ繧ｰ
void Start() { // Rigidbody2D 繧貞叙蠕・
  Rigidbody2D rb = GetComponent<Rigidbody2D>(); 
       if (rb != null) { // 驥榊鴨繧堤┌蜉ｹ蛹悶＠縺ｦ遨ｺ荳ｭ縺ｫ豬ｮ縺九○繧具ｼ郁誠縺｡縺ｪ縺・ｈ縺・↓・・
        rb.gravityScale = 1;
       }
=======
using UnityEngine;

public class DashEnemyDeath : MonoBehaviour
{
    [Header("移動設定")] public float speed = 2f; // 左に移動するスピード
    private bool isDead = false; // 敵が倒されたかどうかのフラグ
    void Start()
    { // Rigidbody2D を取得
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        { // 重力を無効化して空中に浮かせる（落ちないように）
            rb.gravityScale = 1;
        }
>>>>>>> 101f0fd386128ccc887f0f33c258cff8af10126d
    }
    void Update()
    { // 倒されていないときだけ移動処理を行う
        if (!isDead)
        {
            Move();
        }
    }
    void Move()
    { // 常に左方向へ一定速度で移動する
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}