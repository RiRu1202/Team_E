using UnityEngine;

/// <summary>
/// カメラのX移動に合わせて、札も横に動かす（画面内に固定される感じ）
/// </summary>
public class FollowCameraX_t : MonoBehaviour
{
    public Transform targetCamera;     // Main Camera
    public float xOffset = -7f;        // カメラから見た相対位置（左側に出したいならマイナス）
    public bool keepY = true;
    public float fixedY = 0f;

    void LateUpdate()
    {
        if (targetCamera == null) return;

        Vector3 p = transform.position;
        p.x = targetCamera.position.x + xOffset;

        if (keepY)
            p.y = fixedY;

        transform.position = p;
    }
}
