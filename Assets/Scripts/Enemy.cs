using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 속도, 목표, 생존여부를 위한 변수 선언
    public float speed;
    public Rigidbody2D target;

    // 아직 테스트 상태이므로 미리 isLive = true 적용해주기
    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //몬스터가 살아있는 동안에만 움직이도록 조건 추가
        if (!isLive)
            return;

        // 위치 차이 : 타겟 위치 - 나의 위치
        // 방향 : 위치 차이의 정규화 (Normalized)
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        // 플레이어의 키입력 값을 더한 이동 = 몬스터의 방향 값을 더한 이동
        rigid.MovePosition(rigid.position + nextVec);
        // 물리 속도가 이동에 영향을 주지 않도록 속도 제거
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {

        if (!isLive)
            return;
        // 목표의 X축 값과 자신의 X축 값을 비교하여 작으면 true가 되도록 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
}
