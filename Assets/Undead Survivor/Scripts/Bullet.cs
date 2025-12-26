using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 데미지와 관통 변수 선언
    public float damage;
    public int per;
    
    public void init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;

    }


}
