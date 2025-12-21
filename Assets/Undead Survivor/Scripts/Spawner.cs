using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Spawner : MonoBehaviour
{
    // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언;
    public Transform[] spawnPoint;

    float timer;
    //소환 타이머를 위한 변수 선언

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f) {
            timer = 0;
            Spawn();
        }  
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0,2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}