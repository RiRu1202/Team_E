using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnObjectClicked; // UI�֒ʒm����C�x���g

    void OnMouseDown()
    {
        // ���N���b�N����iOnMouseDown�ł͋�ʂł��Ȃ��̂�Input�Ń`�F�b�N�j
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"{gameObject.name} ���N���b�N����܂����I");
            OnObjectClicked?.Invoke(); // �C�x���g�𔭉΁i�ʒm���M�j
        }
    }
}
