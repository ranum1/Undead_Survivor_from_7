using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefadId;
    public float damage;
    public int count;
    // speed 값은 연사속도를 의미 : 적을 수록 많이 발사사
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame

    void Start()
    {
        Init();
    }

    void Update()
    {
        // Update 로직도 switch 문 활용하여 무기마다 로직 실행
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            // 그 외 나머지 경우가 있다면 default ~ break 으로 감싸기
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // 테스트 코드
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        // 속성 변경과 동시에 근접무기의 경우 배치도 필요하니 함수 호출 
        if (id == 0)
            Batch();
    }

    public void Init()
    {
        // 무기 ID에 따라 로직을 분리할 switch 문 작성
        // 무기 ID 하나씩 case ~ break 으로 감싸기
        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;

            // 그 외 나머지 경우가 있다면 default ~ break 으로 감싸기
            default:
                speed = 0.3f;
                break;
        }
    }

    // 생성된 무기를 배치하는 함수수
    void Batch()
    {
        // for 문으로 count 만큼 풀링에서 가져오기
        for (int index = 0; index < count; index++)
        {
            // 가져온 오브젝트의 Transform을 지역변수로 저장
            Transform bullet;
            if (index < transform.childCount)
            {
                // index가 아직 childCount 범위 내라면 GetChild 함수로 가져오기 
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefadId).transform;
                bullet.parent = transform;
            }

            // parent 속성을 통해 부모 변경
            bullet.parent = transform;

            // 배치하면서 먼저 위치, 회전 초기화 하기
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            // Rotate 함수로 계산된 각도 적용 
            bullet.Rotate(rotVec);
            // Translate 함수로 자신의 위쪽으로 이동
            // 이동 방향은 Space.World 기준으로로
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity per 

        }
    }

    void Fire()
    {
        // 지정된 목표가 없으면 넘어가는 조건 로직 작성
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        // normalized : 현재 벡터의 방향은 유지하고 크기를 1로 변환된 속성성
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefadId).transform;
        // 기본 생성 로직을 그대로 활용하면서 위치는 플레이어 위치로 지정
        bullet.position = transform.position;
        // FromToRotation : 지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
