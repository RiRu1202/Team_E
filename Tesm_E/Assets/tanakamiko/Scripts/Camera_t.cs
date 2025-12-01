
using UnityEngine;

public class Camera_t:MonoBehaviour
{
    public float scrollSpeed = 2f;

    public Transform player;       //��l��
    public float followSpeed = 5f;

    void Update()

    {

        transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0, 0);

        //�W�����v�������㉺�Ǐ]
        if(player == null)return;

        Rigidbody2D rb=player.GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity.y != 0)
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y,player.position.y,followSpeed*Time.deltaTime);
            transform.position = pos;
        }
    }

}
