using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언;
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    // 만든 클래스를 그대로 타입으로 활용하여 배열 변수 선언

    int level;
    float timer;
    //소환 타이머를 위한 변수 선언

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime) {
            timer = 0;
            Spawn();
        }  
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
    // 추가할 속성들 : 스프라이트 타입, 소환 시간, 체력, 속도
}