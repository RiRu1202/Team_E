using UnityEngine;

public class MainCamera:MonoBehaviour
{

    public float rightLimit = 0.0f;    //右スクロールリミット

   // start is called before the first frame update
    private void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {

        GameObject player = null;
        player= GameObject.FindGameObjectWithTag("Player");//プレイヤーを探す


        if (player != null)
        {
            //カメラの更新座標
            float x=player.transform.position.x;

            //横同期させる
            if (x > rightLimit)
            {
                x = rightLimit;
            }
            //カメラの位置のVector3を作る
            Vector3 v3=new Vector3(0.0f,0,0);
            transform.position += v3;
        }
    }
}
