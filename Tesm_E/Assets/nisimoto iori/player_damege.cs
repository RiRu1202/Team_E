using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock:MonoBehaviour
{
    public GameObject objPrefab;    //発生させるプレハブデータ
    public float delayTime = 3.0f;  //遅延速度
    public float firespeed = 4.0f;  //発射速度
    public float length = 8.0f;     //範囲

    GameObject player;
}
