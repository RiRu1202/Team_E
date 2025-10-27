using UnityEngine;
using UnityEngine.SceneManagement;//�V�[���̐؂�ւ��ɕK�v
public class PlayerController:MonoBehaviour
{
    Rigidbody2D rbody;       //Rigidbody2D�^�̕ϐ�
    public float speed = 3.0f;
    public float gameOverY = -10f;//�Q�[���I�[�o�[�Ɣ��f����Y���W

    public static string gameState = "playing";  //�Q�[���̏��

    void Start()
    {
        Application.targetFrameRate = 60;

        //Rigidbody2D������Ă���
        rbody = GetComponent<Rigidbody2D>();
        gameState = "playing";  //�Q�[�����ɂ���
    }
    void Update()
    {
        if(gameState!="playing")
        {
            return;
        }

        //�v���C���[��Y���W���ݒ�l���Ⴍ�Ȃ�����
        if(transform.position.y<gameOverY)
        {
            GameOver();
        }
        
    }

    private void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }
        //���x���X�V����
        rbody.linearVelocity = new Vector2(5.0f, 0);
    }
    //�Q�[���I�[�o�[
    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[�I");
        //�uGameOver�v�V�[����ǂݍ���
        SceneManager.LoadScene("GameOva");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("Clear");
    }

}
