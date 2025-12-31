using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour

{
    //무기 ID, 프리펩 ID, 데미지, 개수, 속도 변수 선언
    public int id;
    public int prefabId;
    public float damage;
    public int count; // 몇 개를 배치할 것이냐의 속성
    public float speed; // 근전 무기 회전 속도

    void Start()
    {
        init();
    }


    void Update()
    {
    
    }

    public void init()
    {
        switch (id) {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        //for문으로 count만큼 풀링에서 가져오기
        for (int index=0; index < count; index++){
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;
            //parent 속성을 통해 부모 변경
            bullet.GetComponent<Bullet>().init(damage, -1); // -1 을 넣은 이유는 무한으로 관통할 것입니다의 의미. -1 is Infinity per.
        }

    }
}