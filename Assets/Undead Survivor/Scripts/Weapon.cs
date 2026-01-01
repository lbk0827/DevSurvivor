using UnityEngine;

public class Weapon : MonoBehaviour

{
    //무기 ID, 프리펩 ID, 데미지, 개수, 속도 변수 선언
    public int id;
    public int prefabId;
    public float damage;
    public int count; // 몇 개를 배치할 것이냐의 속성
    public float speed; // 근전 무기 회전 속도

    float timer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init();
    }


    void Update()
    {
        switch (id) {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
               timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Fire();
                    //speed 보다 커지면 초기화하면서 발사 로직 생성
                }
                break;
        }     

        // .. TEST CODE ..
        if (Input.GetButtonDown("Jump")) {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
    
        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch (id) {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    void Batch()
    {
        //for문으로 count만큼 풀링에서 가져오기
        for (int index=0; index < count; index++){
            Transform bullet;
            //자신의 자식 오브젝트 개수 확인은 childCount 속성
            if (index < transform.childCount) {
                bullet = transform.GetChild(index); // index가 아직 childCount 범위 내라면 GetChild 함수로 가져오기
            }
            else {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
                            //parent 속성을 통해 부모 변경
            }
            
            // 위치와 회전 초기화 (풀링에서 가져온 오브젝트의 이전 값 제거)
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World); // 이동 방향은 Space.world 기준
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 을 넣은 이유는 무한으로 관통할 것입니다의 의미. -1 is Infinity per.
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}