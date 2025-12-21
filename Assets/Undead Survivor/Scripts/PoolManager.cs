using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리펩들을 보관할 배열 변수 필요.
    public GameObject[] prefabs;

    // .. 풀 담당을 하는 리스트들이 필요. (프리펩을 생성했을 때, 담을 리스트)
    List<GameObject>[] pools;
    // 참고 - 프리펩 하나와 리스트 하나. 1:1 관계이니, 프리펩이 2개면,리스트도 2개여야함.

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        // for문 (반복문) : 시작문;  조건문;  증감문
        for (int index = 0; index < pools.Length; index++) {// index는 0부터 시작합니다.
        pools[index] = new List<GameObject>();

        }
    }
    // 게임 오브젝트를 반환하는 함수 선언 //  어떤 풀에 들어있는 오브젝트를 가져올 것이냐? -> 가져올 오브젝트 종류를 결정하는 매게 변수 추가.
    public GameObject Get(int index)
    {
        //게임 오브젝트 지역 변수와 리턴을 미리 작성.
        GameObject select = null;


        //... 선택한 풀의 놀고있는 (=비활성화된) 게임오브젝트 접근
        foreach (GameObject item in pools[index])
        {   
            if (!item.activeSelf) {//내용물 오브젝트가 비활성화(대기 상태)인지 확인.
            //... 발견하면 select 변수에 할당(=해당 변수에 집어 넣게는 소리.)       
            select = item;
            select.SetActive(true);
            break;     
            }
        }

        //... 못 찾았으면..?
        if (!select)
            {
             //... 새롭게 생성하고 select 변수에 할당.// Instantiate는 오리지널 게임 오브젝트를 하나 복자해서 장면에 딱 둔다.
             //... 생성된 오브젝트는 해당 오브젝트 풀 리스트에 Add 함수로 추가.
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
            }


        // foreach : 배열, 리스트들의 데이터를 순차적으로 접근하는 반복문.

        return select;
        }
}