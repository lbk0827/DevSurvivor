using System.Collections;
using System.Collections.Generic;
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

        Debug.Log(pools.Length);
        }
    }
}
