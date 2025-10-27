using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  //UI���g���̂ɕK�v
public class GameManager:MonoBehaviour
{
    public GameObject mainImage;      //�摜������GameObject
    public string gameOverSceneName = "GameOva";

    Image titleImage;                 //�摜��\�����Ă���Image�R���|�[�l���g

    //+++���Ԑ����ǉ�+++
    public GameObject timeBar;        //���ԕ\���C���[�W
    public GameObject timeText;       //���ԃe�L�X�g
    TimeController timeCnt;           //TimeController

    void Start()
    {
        //�摜���\���ɂ���
        Invoke("InactiveImage", 1.0f);
        
        //+++���Ԑ����ǉ�+++
        //TimeController���擾
        timeCnt = GetComponent<TimeController>();
        if (timeCnt != null)
        { 
           if (timeCnt.gameTime==0.0f)
           {
               timeBar.SetActive(false);  //�������ԂȂ��Ȃ�B��
           }
        }
    }
    void Update()
    {
        
         if (PlayerController.gameState=="playing")
        {
            //�Q�[����
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerController���擾����
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            //+++���Ԑ����ǉ�+++
            //�^�C�����X�V����
            if (timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    //�����ɑ�����邱�Ƃŏ�����؂�̂Ă�
                    int time=(int)timeCnt.displayTime;
                    //�^�C���X�V
                    if(timeText!=null)
                        timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();
                    //�^�C���I�[�o�[
                    if(time <= 0)
                    {
                       
                       GameOver();  //�Q�[���I�[�o�[�ɂ���
                    }
                }
            }
         }
    }
    void GameOver()
    {
        Debug.Log("GameOva");
        //�V�[����؂�ւ���
        SceneManager.LoadScene(gameOverSceneName);
    }
}
