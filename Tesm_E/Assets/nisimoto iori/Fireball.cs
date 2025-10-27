using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position, transform.rotation);
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = transform.forward * fireballSpeed;
            }
            Destroy(fireball, 3f); 
        }
    }
}

