using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FireController_P : MonoBehaviour 
{ 
    public GameObject objPrefab; 
    public float delayTime = 1f;
    public float fireSpeed = 4.0f; 
    public string targetUIButtonName = "Fire_Frag"; // 判定したいUIの名前をセット
    private Transform gateTransform;
    private float passedTime = 0f;
    void Start() 
    { 
        gateTransform = transform.Find("playergate"); 
    }
    
    void Update() 
    { 
        if (objPrefab == null) return;
        passedTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) 
        { 
            if (IsClickedUI(targetUIButtonName)) 
            { 
                if (passedTime >= delayTime) 
                {
                    Fire(); passedTime = 0f;
                }
            } 
        } 
    }
    
    private bool IsClickedUI(string targetUIName) 
    { 
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition; 
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        foreach (RaycastResult result in results) 
        { 
            if (result.gameObject.name == targetUIName) 
            {
                return true;
            } 
        } 
        return false; 
    } 
    
    public void Fire() 
    {
        if (gateTransform == null) return; 
        Vector2 pos = gateTransform.position;
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity); 
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>(); 
        if (rbody != null) 
        { 
            Vector2 dir = gateTransform.right; rbody.AddForce(dir * fireSpeed, ForceMode2D.Impulse); 
        }
    }
}