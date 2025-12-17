using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //속도, 목표, 생존 여부를 위한 변수 선언.
    public float speed;
    public Rigidbody2D target;   //물리적으로 따라갈거니까 Rigidbody2d Target 설정.

    bool isLive; //이 몬스터가 살아있는지 죽어있는지 분별해주는 isLive라는 bool 변수까지 추가.

    //리지드바디2D와 스프라이트렌더러를 위한 변수 선언
    Rigidbody2D rigid;
    SpriteRenderer spriter;


    // void Awake는 초기화 진행, 컴포넌트 사용하기 위해 GetComponent<타입>(); 선언. 
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // 물리적인 이동이기 때문에 우리는 Update를 쓰지 않고, FixedUpdate();를 사용한다.
    void FixedUpdate() 
    {
        //벡터의 연산으로 몬스터가 이동하는 방향 (플레이어가 있는 쪽으로 향함)을 구할 것임.
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        // 위치 차이 = 타겟 위치 - 나의 위치
        // 방향 = 위치 차이의 정구화(Normalized)
        // 프레임의 영향으로 결과가 달라지지 않도록 FixedDeltaTime 사용.
        // 플레이어의 키입력 값을 더한 이동 = 몬스터의 방향 값을 더한 이동. 이라고 말할 수 있음.
        // rigid에 velocity 값이 존재하면 충돌 시 멀리 날아가버리는데, 물리 속도가 이동에 영향을 주지 안도록 속도 제거 (velocity = vector2.zero)
        
    }

}
