using UnityEngine;

public class MainCamera:MonoBehaviour
{

    public float rightLimit = 0.0f;    //�E�X�N���[�����~�b�g

   // start is called before the first frame update
    private void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {

        GameObject player = null;
        player= GameObject.FindGameObjectWithTag("Player");//�v���C���[��T��


        if (player != null)
        {
            //�J�����̍X�V���W
            float x=player.transform.position.x;

            //������������
            if (x > rightLimit)
            {
                x = rightLimit;
            }
            //�J�����̈ʒu��Vector3�����
            Vector3 v3=new Vector3(0.0f,0,0);
            transform.position += v3;
        }
    }
}
