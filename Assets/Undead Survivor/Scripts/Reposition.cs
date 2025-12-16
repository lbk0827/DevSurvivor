using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
     if (!collision.CompareTag("Area")) 
        return;

        Vector3 playerPos = GameManager.instance.player.transform.position;    //플레이여의 포지션
        Vector3 myPos = transform.position;        //나의 포지션 (타일 맵)
        float diffX = Mathf.Abs(playerPos.x - myPos.x); //(플레이어 위치 - 타일맵 위치)의 절대 값을 계산하여 거리 구하기
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        //switch ~ case : 값의 상태에 따라 로직을 나눠주는 키워드
        //두 오브젝트의 거리 차이에서 x축이 y축 보다 크면 수평 이동
        switch (transform.tag){
            case "Ground":
                if (diffX > diffY) {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY) {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
        }
    }
}